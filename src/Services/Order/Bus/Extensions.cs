using Application.StateMachine;
using Bus.Consumer;
using Confluent.Kafka;
using cShop.Contracts.Services.IdentityServer;
using cShop.Contracts.Services.Order;
using MassTransit;

namespace Bus;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(t =>
        {
            t.SetKebabCaseEndpointNameFormatter();

            t.UsingInMemory();
            
            t.AddRider(r =>
            {
                r.AddProducer<DomainEvents.OrderSubmitted>(nameof(DomainEvents.OrderSubmitted));
                r.AddProducer<DomainEvents.MakeOrderValidate>(nameof(DomainEvents.MakeOrderValidate));

                r.AddConsumer<CustomerCreatedIntegrationConsumer>();
                r.AddConsumer<ProductCreatedIntegrationConsumer>();
                
                r.AddSagaStateMachine<OrderStateMachine, OrderState>();
                
                r.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                    configurator.TopicEndpoint<IntegrationEvent.CustomerCreatedIntegration>(nameof(IntegrationEvent.CustomerCreatedIntegration), "customers-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<CustomerCreatedIntegrationConsumer>(context);
                        });
                    configurator.TopicEndpoint<cShop.Contracts.Services.Catalog.IntegrationEvent.CatalogCreatedIntegration>(nameof(cShop.Contracts.Services.Catalog.IntegrationEvent.CatalogCreatedIntegration), "catalog-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureConsumer<ProductCreatedIntegrationConsumer>(context);
                        });
                    
                    
                    configurator.TopicEndpoint<DomainEvents.OrderSubmitted>(nameof(DomainEvents.OrderSubmitted), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    
                });
                
            });

        });
        
        action?.Invoke(services);
        return services;
    }
}