using Consul;

namespace cShop.Infrastructure.ServiceDiscovery;

public static class Extensions
{
    public static IServiceCollection AddConsulService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(serviceProvider =>  
        {  
            return new ConsulClient(config =>  
            {  
            });  
        });
        services.AddHostedService<ConsulStartedService>();
        action?.Invoke(services);
        return services;
    }   
    
}