using StackExchange.Redis;

namespace Simu.Api.Shared.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }
    
    public Task SetAsync(string key, string value, TimeSpan expiration) =>
        _db.StringSetAsync(key, value, expiration);

    public async Task<string?> GetAsync(string key) =>
        await _db.StringGetAsync(key);
}