using Bus.Consumer;
using Confluent.Kafka;
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
                
                
                t.AddConsumer<CatalogCreatedDomainEventConsumer>();
                t.AddConsumer<CatalogActiveDomainEventConsumer>();
                t.AddConsumer<CatalogInactiveDomainEventConsumer>();
                
                t.UsingKafka((context, config) =>
                {
                    config.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                    config.TopicEndpoint<DomainEvents.CatalogCreated>(nameof(DomainEvents.CatalogCreated), "catalog-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<CatalogCreatedDomainEventConsumer>(context);
                        });
                    config.TopicEndpoint<DomainEvents.CatalogActivated>(nameof(DomainEvents.CatalogActivated), "catalog-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<CatalogActiveDomainEventConsumer>(context);
                        });
                    config.TopicEndpoint<DomainEvents.CatalogInactivated>(nameof(DomainEvents.CatalogInactivated), "catalog-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<CatalogInactiveDomainEventConsumer>(context);
                        });
                    
                });
            });

        });      
        
        action?.Invoke(services);
        return services;
    }
}