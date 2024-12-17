using StackExchange.Redis;

namespace Simu.Api.Configs.Redis;

public static class RedisConfig
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = new RedisOptions();
         configuration.GetSection(RedisOptions.Redis).Bind(redisOptions);
         services.AddSingleton<IConnectionMultiplexer>(sp =>
             ConnectionMultiplexer.Connect(redisOptions.Host));
         return services;
    }
}