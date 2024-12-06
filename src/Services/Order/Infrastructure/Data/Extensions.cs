using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddDbContextCustom<OrderContext>(configuration, typeof(OrderContext));
        services.AddHostedService<OrderMigrationHostedDb>();
        action?.Invoke(services);
        return services;
    }
}