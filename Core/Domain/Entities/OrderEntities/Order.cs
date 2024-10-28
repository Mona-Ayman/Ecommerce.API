
namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string userEmail,
            ShippingAddress shippingAddress,
            ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethods,
            decimal subtotal,
            string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethods = deliveryMethods;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();          //collection navigational prop
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethods { get; set; }               //ref navigational prop
        public int? DeliveryMethodId { get; set; }
        public decimal Subtotal { get; set; }           //subtotal = items.quantity * price
        public string PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
    }
}
