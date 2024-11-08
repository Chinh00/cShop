using Avro.Specific;
using Confluent.SchemaRegistry;

namespace cShop.Infrastructure.Cdc;

public class ConsumerConfig : Confluent.Kafka.ConsumerConfig
{
    public const string Name = "Kafka";
    
    public string TopicName { get; set; }
    public string BootstrapServers { get; set; }
    public string SchemaRegistryServer { get; set; } = "http://localhost:8085";


    public Func<ISchemaRegistryClient, string, byte[], Task<ISpecificRecord?>> HandlePayload; 
    
    
    public ConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        AllowAutoCreateTopics = true;

    }
}