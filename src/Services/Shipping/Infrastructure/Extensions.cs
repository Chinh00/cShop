using Confluent.SchemaRegistry;
using cShop.Infrastructure.Cdc;
using Infrastructure.Cdc;
using IntegrationEvents;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCdcConsumers(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddKafkaConsumer<ShipperConsumerConfig>((config) =>
        {
            config.Topic = "shipper_cdc_events";
            config.GroupId = "shipper_cdc_events-group";
            config.HandlePayload = async (ISchemaRegistryClient schemaRegistry,string eventName, byte[] payload) =>
            {
                return eventName switch
                {
                    nameof(ShipperCreatedIntegrationEvent) => await payload.AsRecord<ShipperCreatedIntegrationEvent>(schemaRegistry),
                    _ => null
                };
            };
        });
        services.AddKafkaConsumer<OrderConsumerConfig>((config) =>
        {
            config.Topic = "order_cdc_events";
            config.GroupId = "order_cdc_events_shipper-group";
            config.HandlePayload = async (ISchemaRegistryClient schemaRegistry,string eventName, byte[] payload) =>
            {
                return eventName switch
                {
                    nameof(OrderConfirmIntegrationEvent) => await payload.AsRecord<OrderConfirmIntegrationEvent>(schemaRegistry),
                    _ => null
                };
            };
        });
        
        
        
        action?.Invoke(services);
        return services;
    }
}