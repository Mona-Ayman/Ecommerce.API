
namespace Shared.OrderModels
{
    public record OrderRequestDTO
    {
        public string BasketId { get; init; }
        public ShippingAddressDTO shipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }
      
    }
}
