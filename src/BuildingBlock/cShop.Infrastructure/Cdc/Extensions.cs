using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace cShop.Infrastructure.Cdc;

public static class Extensions
{
    public static IServiceCollection AddKafkaConsumer<TConfig>(this IServiceCollection services,
        Action<TConfig> action) where TConfig : BackgroundConsumerConfig
    {
        services.AddOptions<TConfig>().BindConfiguration(BackgroundConsumerConfig.Name).Configure(action);
        services.AddHostedService<BackgroundConsumerService<TConfig>>();
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