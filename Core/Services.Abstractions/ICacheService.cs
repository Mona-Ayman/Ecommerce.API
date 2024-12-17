
namespace Services.Abstractions
{
    public interface ICacheService
    {
        public Task SetCachedValueAsync(string cachKey, object value, TimeSpan duration);
        public Task<string?> GetCachedValueAsync(string cacheKey);
    }
}
