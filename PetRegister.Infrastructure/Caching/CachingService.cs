using Microsoft.Extensions.Caching.Distributed;
using PetRegister.Infrastructure.Services;

namespace PetRegister.Infrastructure.Caching;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
            SlidingExpiration = TimeSpan.FromMinutes(20)
        };
    }

    public async Task<string?> GetAsync(string key)
    {
        return await _cache.GetStringAsync(key); 
    }
    
    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value,  _options);
    }

}