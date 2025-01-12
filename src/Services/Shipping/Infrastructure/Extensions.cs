using Confluent.Kafka;
using Confluent.SchemaRegistry;
using cShop.Infrastructure.Cdc;
using cShop.Infrastructure.Mongodb;
using Infrastructure.Cdc;
using Infrastructure.StateMachine;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransits(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        var mOption = new MongoDbbOption();
        configuration.GetSection(MongoDbbOption.Mongodb).Bind(mOption);
        services.AddMassTransit(e =>
        {
            e.SetKebabCaseEndpointNameFormatter();
            e.UsingInMemory();
            
            e.AddRider(t =>
            {
                
                t.AddProducer<ShipmentCreatedIntegrationEvent>(nameof(ShipmentCreatedIntegrationEvent));
                t.AddProducer<ShipmentPickedIntegrationEvent>(nameof(ShipmentPickedIntegrationEvent));
                t.AddProducer<ShipmentDeliveryIntegrationEvent>(nameof(ShipmentDeliveryIntegrationEvent));
                t.AddProducer<OrderCompleteIntegrationEvent>(nameof(OrderCompleteIntegrationEvent));
                
                
                t.AddSagaStateMachine<ShippingStateMachine, ShippingState, ShippingStateMachineDefinition>().MongoDbRepository(
                    f =>
                    {
                        f.Connection = mOption.ToString();
                        f.DatabaseName = mOption.DatabaseName;
                        f.CollectionName = "ShipmentSaga";
                    });
                
                
                t.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    configurator.TopicEndpoint<ShipmentCreatedIntegrationEvent>(nameof(ShipmentCreatedIntegrationEvent),
                        "shipment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<ShippingState>(context);
                        });
                    configurator.TopicEndpoint<ShipmentPickedIntegrationEvent>(nameof(ShipmentPickedIntegrationEvent),
                        "shipment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<ShippingState>(context);
                        });
                    configurator.TopicEndpoint<ShipmentDeliveryIntegrationEvent>(nameof(ShipmentDeliveryIntegrationEvent),
                        "shipment-group",
                        c =>
                        {
                            c.AutoOffsetReset = AutoOffsetReset.Earliest;
                            c.CreateIfMissing(n => n.NumPartitions = 1);
                            c.ConfigureSaga<ShippingState>(context);
                        });
                });
            });
            
        });
        
        
        action?.Invoke(services);
        return services;
    }
    
    
    
    public static IServiceCollection AddCdcConsumers(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddKafkaConsumer<ShipperConsumerConfig>((config) =>
        {
            config.Topic = "shipper_cdc_events";
            config.GroupId = "shipper_cdc_events_shipping_group";
            config.HandlePayload = async (ISchemaRegistryClient schemaRegistry,string eventName, byte[] payload) =>
            {
                return eventName switch
                {
                    nameof(ShipperCreatedIntegrationEvent) => await payload.AsRecord<ShipperCreatedIntegrationEvent>(
                        schemaRegistry),
                    _ => null
                };
            };
        });
        services.AddKafkaConsumer<OrderConsumerConfig>((config) =>
        {
            config.Topic = "order_cdc_events";
            config.GroupId = "order_cdc_events_shipper_group";
            config.HandlePayload = async (ISchemaRegistryClient schemaRegistry,string eventName, byte[] payload) =>
            {
                return eventName switch
                {
                    nameof(OrderConfirmIntegrationEvent) => await payload.AsRecord<OrderConfirmIntegrationEvent>(
                        schemaRegistry),
                    _ => null
                };
            };
        });                                                 
        
        
        
        action?.Invoke(services);
        return services;
    }
}