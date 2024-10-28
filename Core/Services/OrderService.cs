
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Specifications;
using Shared.OrderModels;

namespace Services
{
    internal class OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper) : IOrderService
    {

        public async Task<OrderResultDTO> CreateOrderAsync(OrderRequestDTO request, string UserEmail)
        {
            // address
            var address = mapper.Map<ShippingAddress>(request.shipToAddress);

            // orderItms
            var basket = await basketRepository.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(AddOrderItemAsync(item, product));
            }

            // deliveryMethod
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodMethodNotFoundException(request.DeliveryMethodId);

            // subTotal
            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);

            // save to db
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var existingOrder = await orderRepo.GetAsync(new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId!));
            if (existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
            }
            var order = new Order(UserEmail, address, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);
            await orderRepo.CreateAsync(order);
            await unitOfWork.SaveChangesAsync();

            // mapping & return
            return mapper.Map<OrderResultDTO>(order);

        }

        private OrderItem AddOrderItemAsync(BasketItem item, Product product)
          => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);

        public async Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodAsync()
        {
            var methods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDTO>>(methods);
        }

        public async Task<OrderResultDTO> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithIncludeSpecifications(id)) ?? throw new OrderNotFoundException(id);
            return mapper.Map<OrderResultDTO>(order);

        }

        public async Task<IEnumerable<OrderResultDTO>> GetOrdersByEmaillAsync(string email)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludeSpecifications(email));
            return mapper.Map<IEnumerable<OrderResultDTO>>(orders);
        }
    }
}
