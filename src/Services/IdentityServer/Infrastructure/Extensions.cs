using Confluent.Kafka;
using cShop.Contracts.Services.IdentityServer;
using MassTransit;

namespace IdentityServer.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(e =>
        {   
            e.SetKebabCaseEndpointNameFormatter();
            e.UsingInMemory();
            
            e.AddRider(t =>
            {
                t.AddProducer<IntegrationEvent.CustomerCreatedIntegration>(nameof(IntegrationEvent.CustomerCreatedIntegration));
                
                t.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                });
                
            });
        });
        
        action?.Invoke(services);
        return services;
    }
}