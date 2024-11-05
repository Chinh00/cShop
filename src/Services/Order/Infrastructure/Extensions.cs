using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using cShop.Contracts.Services.Payment;
using cShop.Infrastructure.Cdc;
using cShop.Infrastructure.Projection;
using Infrastructure.StateMachine;
using IntegrationEvents;
using MassTransit;
using OrderSubmitted = cShop.Contracts.Services.Order.OrderSubmitted;

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
                r.AddProducer<OrderSubmitted>(nameof(OrderSubmitted));
                r.AddProducer<MakeOrderValidate>(nameof(MakeOrderValidate));
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
                    
                    
                    configurator.TopicEndpoint<OrderSubmitted>(nameof(OrderSubmitted), "orders-group",
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
                    configurator.TopicEndpoint<IntegrationEvent.BasketCheckoutFail>(nameof(cShop.Contracts.Services.Basket.IntegrationEvent.BasketCheckoutFail), "basket-group",
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
            e.HandlePayload = async (schemaRegistryClient, eventName, payload) =>
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