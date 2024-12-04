using Confluent.Kafka;
using cShop.Contracts.Services.Catalog;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

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
                
                
                
                t.AddProducer<OrderStockChangedIntegrationEvent>(nameof(OrderStockChangedIntegrationEvent));
                t.AddProducer<OrderStockUnavailableIntegrationEvent>(nameof(OrderStockUnavailableIntegrationEvent));
                
                
                
                t.AddProducer<OrderStockValidatedFailIntegrationEvent>(nameof(OrderStockValidatedFailIntegrationEvent));
                t.AddProducer<OrderStockValidatedSuccessIntegrationEvent>(nameof(OrderStockValidatedSuccessIntegrationEvent));
                
                


                t.AddConsumer<EventDispatcher>();
                
                
                
                t.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    config.TopicEndpoint<MakeOrderStockValidateIntegrationEvent>(nameof(MakeOrderStockValidateIntegrationEvent), "catalog-groups",
                        configurator =>
                        {
                            configurator.CreateIfMissing(e => e.NumPartitions = 1);
                            configurator.AutoOffsetReset = AutoOffsetReset.Earliest;
                            configurator.ConfigureConsumer<EventDispatcher>(context);
                        });
                    
                });
            });

        });      
        
        action?.Invoke(services);
        return services;
    }
}