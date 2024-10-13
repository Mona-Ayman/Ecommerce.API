
namespace Services.Abstractions
{
    public interface IBasketService
    {
        public Task<bool> DeleteBasketAsync(string id);
        public Task<BasketDTO?> GetBasketAsync(string id);
        public Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket);
    }
}
