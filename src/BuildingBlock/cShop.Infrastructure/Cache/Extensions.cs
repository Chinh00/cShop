using cShop.Infrastructure.Cache.Redis;

namespace cShop.Infrastructure.Cache;

public static class Extensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddOptions<RedisOptions>().Bind(configuration.GetSection(RedisOptions.Name));
        services.AddSingleton<IRedisService, RedisService>();
        
        action?.Invoke(services);
        return services;
    }
}