using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;

namespace cShop.Infrastructure.Cdc;

public class BackgroundConsumerConfig : ConsumerConfig
{
    public const string Name = "Kafka";
    
    public string Topic { get; set; }
    public string SchemaRegistryServer { get; set; } = "http://localhost:8085";


    public Func<ISchemaRegistryClient, string, byte[], Task<ISpecificRecord>> HandlePayload; 
    
    
    public BackgroundConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        AllowAutoCreateTopics = true;

    }
}