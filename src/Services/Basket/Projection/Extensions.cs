using cShop.Infrastructure.Projection;

namespace Projection;

public static class Extensions
{
    public static IServiceCollection AddProjectionCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddProjections(configuration)
            .AddScoped<IProjectionDbContext, BasketProjectionDbContext>()
            .AddScoped<IProjectionRepository<BasketProjection>, BasketProjectionRepository>();
        
        
        action?.Invoke(services);
        return services;
    }
}