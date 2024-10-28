global using Product = Domain.Entities.Product;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Specifications;
using Shared;
using Stripe;
using Stripe.Forwarding;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("StripeSettings")["SecretKey"];

            // get basket
            var basket = await basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);
            //return await unitOfWork.GetRepository<Product, int>().GetAsync(1) ?? throw new ProductNotFoundException(1);
            // get product to check the real price
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(1) ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }

            //check if there is a delivery method 
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method was selected");
            // get delivery method 
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value) ?? throw new DeliveryMethodMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;

            // get amount that will be paid to send it to stripe
            var amount = (long)(basket.Items.Sum(item => item.Quantity * item.Price) + basket.ShippingPrice) * 100;

            var service = new PaymentIntentService();
            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                // create payment intent
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //update values of existing payment intent
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            await basketRepository.UpdateBasketAsync(basket);
            return mapper.Map<BasketDTO>(basket);

        }

        public async Task UpdateOrderPaymentStatus(string request, string StripeHeader)
        {
            var stripeEvent = EventUtility.ConstructEvent(request,
                StripeHeader, configuration.GetRequiredSection("StripeSettings")["EndPointSecret"]);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentStatusRecieved(paymentIntent.Id);

                    break;
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentStatusFailed(paymentIntent.Id);
                    break;
                // ... handle other event types
                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithPaymentIntentSpecifications(paymentIntentId))
                 ?? throw new Exception();
            order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
        private async Task UpdatePaymentStatusRecieved(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithPaymentIntentSpecifications(paymentIntentId))
                ?? throw new Exception();
            order.PaymentStatus = OrderPaymentStatus.PaymentRecieved;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
