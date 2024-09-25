using Confluent.Kafka;
using cShop.Contracts.Events.DomainEvents;
using cShop.Infrastructure.Bus;
using MassTransit;

namespace MessageBus;

public static class Extensions
{

    public static IServiceCollection AddCustomMasstransit(this IServiceCollection services, IConfiguration config)
    {

        services.AddMessageBus(config);
        
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            
            cfg.UsingInMemory();
            
            cfg.AddRider(rider =>
            {
                rider.AddProducer<ProductCreatedDomainEvent>(nameof(ProductCreatedDomainEvent));
                rider.AddProducer<ProductNameUpdatedDomainEvent>(nameof(ProductNameUpdatedDomainEvent));
                
                
                rider.UsingKafka((context, k) =>
                {
                    
                    k.Host(config.GetValue<string>("Kafka:BootstrapServers"));
                    
                    k.TopicEndpoint<ProductCreatedDomainEvent>(nameof(ProductCreatedDomainEvent), "product", c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.CreateIfMissing(e => e.NumPartitions = 1);
                    });
                    
                    k.TopicEndpoint<ProductNameUpdatedDomainEvent>(nameof(ProductNameUpdatedDomainEvent), "product", c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.CreateIfMissing(e => e.NumPartitions = 1);
                    });
                    
                    
                });
                
            });
            
            
        });
        
        return services;
    }
}