
namespace Shared.OrderModels
{
    public record OrderResultDTO
    {
        public Guid Id { get; set; }
        public string UserEmail { get; init; }
        public ShippingAddressDTO ShippingAddress { get; init; }
        public ICollection<OrderItemDTO> OrderItems { get; init; } = new List<OrderItemDTO>();
        public string PaymentStatus { get; init; }
        public string DeliveryMethods { get; init; }
        public decimal Subtotal { get; init; }
        public string PaymentIntentId { get; init; } = string.Empty;
        public DateTimeOffset OrderDate { get; init; }
        public decimal Total { get; init; }

    }
}
