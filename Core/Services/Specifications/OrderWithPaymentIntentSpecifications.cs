
using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) 
            : base(order => order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
