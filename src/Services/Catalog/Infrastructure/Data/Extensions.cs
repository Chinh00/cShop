using cShop.Core.Repository;
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddDbContextCustom<CatalogContext>(configuration, typeof(CatalogContext));
        services.AddHostedService<CatalogMigrationHostedDb>();
        action?.Invoke(services);
        return services;
    }
}