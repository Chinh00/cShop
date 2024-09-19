namespace cShop.Infrastructure.Bus.Kafka;

public sealed class KafkaConfig
{
    public const string Kafka = "Kafka";
    
    public string BootstrapServers { get; set; } = "localhost:9092";
    
}