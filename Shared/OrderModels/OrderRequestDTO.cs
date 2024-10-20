
namespace Shared.OrderModels
{
    public record OrderRequestDTO
    {
        public string BasketId { get; init; }
        public ShippingAddressDTO shippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
      
    }
}
