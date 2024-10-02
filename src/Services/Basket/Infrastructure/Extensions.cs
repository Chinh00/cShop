using GrpcServices;
namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCatalogGrpcClient(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddGrpcClient<Catalog.CatalogClient>(o => o.Address = new Uri(configuration.GetValue<string>("CatalogGrpc:Url"))); 
        action?.Invoke(services);
        return services;
    }
}