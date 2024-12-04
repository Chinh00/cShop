using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddDbContextCustom<ShipperContext>(configuration, typeof(ShipperContext));
        services.AddHostedService<ShipperMigrationHostedDb>();
        action?.Invoke(services);
        return services;
    }
}