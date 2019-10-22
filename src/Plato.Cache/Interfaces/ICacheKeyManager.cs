using System.Threading.Tasks;

namespace Plato.Cache.Interfaces
{
    public interface ICacheKeyManager
    {
        ICache Cache { get; }
        Task<string> GenerateAsync<TData>(CacheKey<TData> cacheKey, TData data);
        Task ClearAsync<TData>(TData data);
    }
}