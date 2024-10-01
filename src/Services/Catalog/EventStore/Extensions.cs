using cShop.Infrastructure.EventStore;

namespace EventStore;

public static class Extensions
{
    public static IServiceCollection AddEventStoreCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddEventStoreDbContext<CatalogEventStoreDbContext>(configuration)
            .AddScoped<IEventStoreRepository, CatalogEventStoreRepository>();
        
        action?.Invoke(services);
        return services;
    }
}