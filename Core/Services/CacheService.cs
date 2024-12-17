
using Domain.Contracts;

namespace Services
{

    internal class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedValueAsync(string cacheKey)
         => await cacheRepository.GetAsync(cacheKey);

        public async Task SetCachedValueAsync(string cachKey, object value, TimeSpan duration)
         => await cacheRepository.SetAsync(cachKey, value, duration);
    }
}
