using cShop.Contracts.Services.Catalog;
using MassTransit;

namespace Bus;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(k =>
        {
            k.SetKebabCaseEndpointNameFormatter();

            k.UsingInMemory();
            
            
            k.AddRider(t =>
            {
                t.AddProducer<DomainEvents.CatalogCreated>(nameof(DomainEvents.CatalogCreated));
                t.AddProducer<DomainEvents.CatalogActivated>(nameof(DomainEvents.CatalogActivated));
                t.AddProducer<DomainEvents.CatalogInactivated>(nameof(DomainEvents.CatalogInactivated));
                
                t.AddProducer<IntegrationEvent.CatalogCreatedIntegration>(nameof(IntegrationEvent.CatalogCreatedIntegration));
                
              
                
                t.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                    
                });
            });

        });      
        
        action?.Invoke(services);
        return services;
    }
}