using Confluent.SchemaRegistry;

namespace cShop.Infrastructure.SchemaRegistry;

public static class Extensions
{
    public static IServiceCollection AddSchemaRegistry(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddSingleton<ISchemaRegistryClient>(t => new CachedSchemaRegistryClient(new SchemaRegistryConfig()
        {
            Url = configuration["SchemaRegistry:Url"],
        }));
        
        action?.Invoke(services);
        return services;
    }
}