using cShop.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EventStore;

public static class Extensions
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection>? action = null)
    {

        services
            .AddEventStoreDbContext<CatalogEventStoreDbContext>(configuration)
            .AddHostedService<DbContextMigrateHostedService<CatalogEventStoreDbContext>>();

        
        //services.AddScoped(e => e.GetService<DatabaseFacade>());

        services.AddScoped<IEventStoreRepository, CatalogEventStoreRepository>();
        action?.Invoke(services);   
        return services;
    }
}