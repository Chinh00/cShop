using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace cShop.Infrastructure.Cdc;

public static class Extensions
{
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services,
        Action<BackgroundConsumerConfig> action)
    {        
        services.AddOptions<BackgroundConsumerConfig>().BindConfiguration(BackgroundConsumerConfig.Name).Configure(action);
        services.AddHostedService<BackgroundConsumerService>();
        return services;
    }

    public static async Task<ISpecificRecord?> AsRecord<TConvert>(this byte[] payload, ISchemaRegistryClient schemaRegistryClient)
        where TConvert : ISpecificRecord
    {
        ISpecificRecord record = null;
        var deserialize = new AvroDeserializer<TConvert>(schemaRegistryClient);
        record = await deserialize.DeserializeAsync(payload, false, new SerializationContext());
        return record;
    }
}