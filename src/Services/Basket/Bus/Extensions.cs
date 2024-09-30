using Bus.Consumer;
using Confluent.Kafka;
using cShop.Contracts.Services.Basket;
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
            
            
            k.AddRider(r =>
            {
                r.AddProducer<DomainEvents.BasketCreated>(nameof(DomainEvents.BasketCreated));
                r.AddProducer<DomainEvents.BasketItemAdded>(nameof(DomainEvents.BasketItemAdded));



                r.AddConsumer<BasketCreatedDomainEventConsumer>();
                
                r.UsingKafka((context, o) =>
                {
                    o.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    o.TopicEndpoint<DomainEvents.BasketCreated>(nameof(DomainEvents.BasketCreated), "basket", c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.CreateIfMissing(t => t.NumPartitions = 1);
                        c.ConfigureConsumer<BasketCreatedDomainEventConsumer>(context);
                    });
                    
                    
                }); 
            });

        });  
        
        action?.Invoke(services);
        return services;
    }
}