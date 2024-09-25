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

        action?.Invoke(services);
        return services;
    }
}