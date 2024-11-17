using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using cShop.Contracts.Services.Order;
using cShop.Contracts.Services.Payment;
using cShop.Infrastructure.Cdc;
using cShop.Infrastructure.Projection;
using Infrastructure.StateMachine;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

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
                r.AddProducer<OrderStartedIntegrationEvent>(nameof(OrderStartedIntegrationEvent));
                r.AddProducer<MakeOrderStockValidateIntegrationEvent>(nameof(MakeOrderStockValidateIntegrationEvent));
                
                
                r.AddProducer<PaymentProcessSuccess>(nameof(PaymentProcessSuccess));
                r.AddProducer<PaymentProcessFail>(nameof(PaymentProcessFail));
                
                
                r.AddProducer<OrderConfirmed>(nameof(OrderConfirmed));
                r.AddProducer<OrderCancelled>(nameof(OrderCancelled));
                

                
                

                r.AddSagaStateMachine<OrderStateMachine, OrderState, OrderStateMachineDefinition>()
                    .MongoDbRepository(e =>
                {
                    e.Connection = mongodb.ToString();
                });
                
                r.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    
                    
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
                    configurator.TopicEndpoint<PaymentProcessSuccess>(nameof(PaymentProcessSuccess), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<PaymentProcessFail>(nameof(PaymentProcessFail), "payment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(e => e.NumPartitions = 1);
                            c.ConfigureSaga<OrderState>(context);
                        });
                    configurator.TopicEndpoint<OrderConfirmed>(nameof(OrderConfirmed), "order-group",
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
                    
                });
                
            });

        });
        
        action?.Invoke(services);
        return services;
    }

    public static IServiceCollection AddCdcConsumer(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        
        services.AddKafkaConsumer(e =>
        {
            e.TopicName = "catalog_cdc_events";
            e.GroupId = "catalog_cdc_events-group";
            e.HandlePayload = async (ISchemaRegistryClient schemaRegistryClient, string eventName, byte[] payload) =>
            {
                ISpecificRecord result = null;    
                
            

                
                switch (eventName)
                {
                    case nameof(ProductCreated):
                    {
                        var deserialize = new AvroDeserializer<ProductCreated>(schemaRegistryClient);

                        result = await deserialize.DeserializeAsync(payload, false, new SerializationContext());
                        
                        break;
                    }
                }
                
                
                
                return result;
            };
        });
        
        
        action?.Invoke(services);
        return services;
    }
    
    
}