using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer)   // we must install StackExchange.Redis package 
        : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
       => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            if (basket.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLiv = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLiv ?? TimeSpan.FromDays(30));
            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
