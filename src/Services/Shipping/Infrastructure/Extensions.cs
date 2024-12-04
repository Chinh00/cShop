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
        
        
        action?.Invoke(services);
        return services;
    }
}