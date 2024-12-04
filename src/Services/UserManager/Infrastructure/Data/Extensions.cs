using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddDbContextCustom<UserContext>(configuration, typeof(UserContext));
        services.AddHostedService<UserMigrationHostedDb>();

        
        
        action?.Invoke(services);
        return services;
    }
}