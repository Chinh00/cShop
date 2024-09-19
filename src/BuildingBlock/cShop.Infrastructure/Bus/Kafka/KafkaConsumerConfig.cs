using Confluent.Kafka;

namespace cShop.Infrastructure.Bus.Kafka;

public class KafkaConsumerConfig : ConsumerConfig 
{
    public string TopicName { get; set; }
    
    public string GroupId { get; set; }
    
    public Func<string, string, string, IConsumer<byte[], byte[]>> EventHandler { get; set; }


    public KafkaConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Latest;
        
    }
}