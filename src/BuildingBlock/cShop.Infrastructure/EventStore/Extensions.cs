using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.EventStore;

public static class Extensions
{
    public static IServiceCollection AddEventStoreDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
        where TDbContext : EventStoreDbContextBase
    {

        services.AddDbContext<TDbContext>((provider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("EventStore"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });

        
        services.AddHostedService<DbContextMigrateHostedService<TDbContext>>();
        
        action?.Invoke(services);
        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type type,
        Action<IServiceCollection>? action = null)
    {
        services.Scan(e =>
        {
            e.FromTypes(type).AddClasses(t => t.AssignableTo<IEventStoreRepository>()).AsImplementedInterfaces().WithTransientLifetime();
        });
        
        
        action?.Invoke(services);
        return services;
    }
    
}