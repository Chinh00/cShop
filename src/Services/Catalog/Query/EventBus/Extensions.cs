using cShop.Contracts.Events.DomainEvents;
using cShop.Infrastructure.Bus;
using EventBus.Consumers;
using MassTransit;

namespace EventBus;

public static class Extensions
{
    public static IServiceCollection AddConsumerCustom(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection>? action = null)
    {


        
        
        
        action?.Invoke(services);
        
        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection>? action = null)
    {
        
        
        
        action?.Invoke(services);
        return services;
    }
    
    public static IServiceCollection AddCustomMasstransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            
            cfg.UsingInMemory();
            
            cfg.AddRider(rider =>
            {
                rider.AddConsumer<ProductCreatedDomainEventConsumer>();
                rider.AddConsumer<ProductUpdatedDomainEventConsumer>();
                
                
                rider.UsingKafka((context, k) =>
                {
                    k.Host(config.GetValue<string>("Kafka:BootstrapServers"));
                    k.TopicEndpoint<ProductCreatedDomainEvent>(nameof(ProductCreatedDomainEvent), "product-consumer",
                        c =>
                        {
                            c.ConfigureConsumer<ProductCreatedDomainEventConsumer>(context);
                        } );
                    k.TopicEndpoint<ProductNameUpdatedDomainEvent>(nameof(ProductNameUpdatedDomainEvent), "product-consumer",
                        c =>
                        {
                            c.ConfigureConsumer<ProductUpdatedDomainEventConsumer>(context);
                        } );
                    
                    
                    
                });
            });
            
        });
        
        
        action?.Invoke(services);
        return services;
    }
    
}