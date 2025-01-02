using Confluent.Kafka;
using Confluent.SchemaRegistry;
using cShop.Contracts.Services.Order;
using cShop.Infrastructure.Cdc;
using cShop.Infrastructure.Mongodb;
using Infrastructure.Cdc;
using Infrastructure.StateMachine;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        var mOption = new MongoDbbOption();
        configuration.GetSection(MongoDbbOption.Mongodb).Bind(mOption);
        services.AddMassTransit(t =>
        {
            t.SetKebabCaseEndpointNameFormatter();

            t.UsingInMemory();
            t.AddRequestClient<CheckOrder>();
            
            t.AddRider(r =>
            {
                r.AddProducer<OrderStartedIntegrationEvent>(nameof(OrderStartedIntegrationEvent));
                
                r.AddProducer<MakeOrderStockValidateIntegrationEvent>(nameof(MakeOrderStockValidateIntegrationEvent));
                
                
                r.AddProducer<PaymentProcessSuccessIntegrationEvent>(nameof(PaymentProcessSuccessIntegrationEvent));
                r.AddProducer<PaymentProcessFailIntegrationEvent>(nameof(PaymentProcessFailIntegrationEvent));
                
                
                r.AddProducer<OrderPaidIntegrationEvent>(nameof(OrderPaidIntegrationEvent));
                
                r.AddProducer<OrderUnPaidIntegrationEvent>(nameof(OrderUnPaidIntegrationEvent));
                
                r.AddProducer<OrderConfirmed>(nameof(OrderConfirmed));
                
                r.AddProducer<OrderCancelled>(nameof(OrderCancelled));

                r.AddConsumer<EventDispatcher>();
                
                
                r.AddSagaStateMachine<OrderStateMachine, OrderState, OrderStateMachineDefinition>()
                    .MongoDbRepository(e =>
                    {
                        e.Connection = mOption.ToString();
                        e.DatabaseName = mOption.DatabaseName;
                        e.CollectionName = "OrderSaga";
                    });
                
                r.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                    
                    configurator.TopicEndpoint<OrderConfirmed>(nameof(OrderConfirmed), "order-group",
                        endpointConfigurator =>
                        {
                            endpointConfigurator.AutoOffsetReset = AutoOffsetReset.Earliest;
                            endpointConfigurator.CreateIfMissing(e => e.NumPartitions = 1);
                            endpointConfigurator.ConfigureConsumer<EventDispatcher>(context);
                        });
                    
                    
                    configurator.TopicEndpoint<OrderStartedIntegrationEvent>(nameof(OrderStartedIntegrationEvent), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    
                    
                    
                    configurator.TopicEndpoint<OrderStockValidatedSuccessIntegrationEvent>(nameof(OrderStockValidatedSuccessIntegrationEvent), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<OrderStockValidatedFailIntegrationEvent>(nameof(OrderStockValidatedFailIntegrationEvent), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<OrderStockChangedIntegrationEvent>(nameof(OrderStockChangedIntegrationEvent), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<OrderStockUnavailableIntegrationEvent>(nameof(OrderStockUnavailableIntegrationEvent), "orders-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    
                    
                    
                    
                    configurator.TopicEndpoint<BasketCheckoutSuccessIntegrationEvent>(nameof(BasketCheckoutSuccessIntegrationEvent), "basket-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<BasketCheckoutFailIntegrationEvent>(nameof(BasketCheckoutFailIntegrationEvent), "basket-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<PaymentProcessSuccessIntegrationEvent>(nameof(PaymentProcessSuccessIntegrationEvent), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<PaymentProcessFailIntegrationEvent>(nameof(PaymentProcessFailIntegrationEvent), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    
                    configurator.TopicEndpoint<OrderCancelled>(nameof(OrderCancelled), "order-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<OrderCompleteIntegrationEvent>(nameof(OrderCompleteIntegrationEvent), "order-group",
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

    public static IServiceCollection AddCdcConsumer(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddKafkaConsumer<CatalogConsumerConfig>(e =>
        {
            e.Topic = "catalog_cdc_events";
            e.GroupId = "catalog_cdc_events-group";
            e.HandlePayload = async (ISchemaRegistryClient schemaRegistryClient, string eventName, byte[] payload) =>
            {

                return eventName switch
                {
                    nameof(ProductCreated) => await payload.AsRecord<ProductCreated>(schemaRegistryClient),
                    _ => throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null)
                };



            };
        });
        services.AddKafkaConsumer<CustomerConsumerConfig>(e =>
        {
            e.Topic = "customer_cdc_events";
            e.GroupId = "customer_cdc_events-group";
            e.HandlePayload = async (ISchemaRegistryClient schemaRegistryClient, string eventName, byte[] payload) =>
            {
                return  (eventName switch
                {
                    nameof(CustomerCreatedIntegrationEvent) => await payload.AsRecord<CustomerCreatedIntegrationEvent>(schemaRegistryClient),
                    _ => throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null)
                });
            };
        });
        
        action?.Invoke(services);
        return services;
    }
    
    
}