using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using cShop.Infrastructure.Cdc;
using IntegrationEvents;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCdcConsumer(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {


        services.AddKafkaConsumer(e =>
        {
            e.Topic = "order_cdc_events";
            e.GroupId = "order_cdc_events_group";
            e.HandlePayload = async (ISchemaRegistryClient schemaRegistryClient, string eventName, byte[] payload) =>
            {
                ISpecificRecord result = default;
                switch (eventName)
                {
                    case nameof(OrderComplete):
                    {
                        var deserialize = new AvroDeserializer<OrderComplete>(schemaRegistryClient);
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