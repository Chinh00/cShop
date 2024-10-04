using Application.StateMachine;
using Bus.Consumer;
using Confluent.Kafka;
using cShop.Contracts.Services.IdentityServer;
using cShop.Contracts.Services.Order;
using cShop.Infrastructure.Projection;
using MassTransit;

namespace Bus;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        
        var mongodb = configuration.GetSection(MongoDbOptions.MongoDb).Get<MongoDbOptions>();
        services.AddMassTransit(t =>
        {
            t.SetKebabCaseEndpointNameFormatter();

            t.UsingInMemory();
            
            t.AddRider(r =>
            {
                r.AddProducer<DomainEvents.OrderSubmitted>(nameof(DomainEvents.OrderSubmitted));
                r.AddProducer<DomainEvents.MakeOrderValidate>(nameof(DomainEvents.MakeOrderValidate));
                r.AddProducer<IntegrationEvents.PaymentProcessSuccess>(nameof(IntegrationEvents.PaymentProcessSuccess));                
                r.AddProducer<IntegrationEvents.PaymentProcessFail>(nameof(IntegrationEvents.PaymentProcessFail));                
                
                r.AddProducer<DomainEvents.OrderConfirmed>(nameof(DomainEvents.OrderConfirmed));
                r.AddProducer<DomainEvents.OrderCancelled>(nameof(DomainEvents.OrderCancelled));
                
                
                r.AddConsumer<CustomerCreatedIntegrationConsumer>();
                r.AddConsumer<ProductCreatedIntegrationConsumer>();
                
                

                r.AddSagaStateMachine<OrderStateMachine, OrderState, OrderStateMachineDefinition>()
                    .MongoDbRepository(e =>
                {
                    e.Connection = mongodb.ToString();
                });
                
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
                    
                    configurator.TopicEndpoint<cShop.Contracts.Services.Basket.IntegrationEvent.BasketCheckoutSuccess>(nameof(cShop.Contracts.Services.Basket.IntegrationEvent.BasketCheckoutSuccess), "basket-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<cShop.Contracts.Services.Basket.IntegrationEvent.BasketCheckoutFail>(nameof(cShop.Contracts.Services.Basket.IntegrationEvent.BasketCheckoutFail), "basket-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<IntegrationEvents.PaymentProcessSuccess>(nameof(IntegrationEvents.PaymentProcessSuccess), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<IntegrationEvents.PaymentProcessFail>(nameof(IntegrationEvents.PaymentProcessFail), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<DomainEvents.OrderConfirmed>(nameof(DomainEvents.OrderConfirmed), "order-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<DomainEvents.OrderCancelled>(nameof(DomainEvents.OrderCancelled), "order-group",
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