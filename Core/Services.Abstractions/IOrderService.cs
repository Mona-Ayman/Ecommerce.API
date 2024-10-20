
using Shared.OrderModels;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        public Task<OrderResultDTO> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResultDTO>> GetOrdersByEmaillAsync(string email);
        public Task<OrderResultDTO> CreateOrderAsync(OrderRequestDTO request, string UserEmail);
        public Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodAsync();
    }
}
