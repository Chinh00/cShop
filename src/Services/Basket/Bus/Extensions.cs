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

                r.AddProducer<IntegrationEvent.BasketCheckoutSuccess>(nameof(IntegrationEvent.BasketCheckoutSuccess));
                r.AddProducer<IntegrationEvent.BasketCheckoutFail>(nameof(IntegrationEvent.BasketCheckoutFail));
                
                


                r.AddConsumer<BasketCreatedDomainEventConsumer>();
                r.AddConsumer<BasketItemAddedDomainEventConsumer>();

                r.AddConsumer<OrderValidateIntegrationEventConsumer>();
                
                r.UsingKafka((context, o) =>
                {
                    o.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    o.TopicEndpoint<DomainEvents.BasketCreated>(nameof(DomainEvents.BasketCreated), "basket", c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.CreateIfMissing(t => t.NumPartitions = 1);
                        c.ConfigureConsumer<BasketCreatedDomainEventConsumer>(context);
                    });
                    o.TopicEndpoint<DomainEvents.BasketItemAdded>(nameof(DomainEvents.BasketItemAdded), "basket", c =>
                    {
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.CreateIfMissing(t => t.NumPartitions = 1);
                        c.ConfigureConsumer<BasketItemAddedDomainEventConsumer>(context);
                    });
                    
                    o.TopicEndpoint<cShop.Contracts.Services.Order.DomainEvents.MakeOrderValidate>(nameof(cShop.Contracts.Services.Order.DomainEvents.MakeOrderValidate), "order-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<OrderValidateIntegrationEventConsumer>(context);
                        });
                    
                    
                }); 
            });

        });  
        
        action?.Invoke(services);
        return services;
    }
}