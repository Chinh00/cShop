namespace Infrastructure;
using GrpServices;
public static class Extensions
{

    public static IServiceCollection AddGrpcClientCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddGrpcClient<Catalog.CatalogClient>(e => e.Address = new Uri("https://localhost:3000"));
        
        
        
        
        action?.Invoke(services);
        return services;
    }
}