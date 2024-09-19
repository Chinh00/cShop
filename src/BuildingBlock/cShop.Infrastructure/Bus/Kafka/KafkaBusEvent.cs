using System.Text.Json;
using Confluent.Kafka;
using cShop.Core.Domain;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Bus.Kafka;

public class KafkaBusEvent : IBusEvent
{
    private readonly IOptions<KafkaConfig> _kafkaConfig;

    public KafkaBusEvent(IOptions<KafkaConfig> kafkaConfig)
    {
        _kafkaConfig = kafkaConfig;
    }

    public async Task PublishAsync(string[] topics, IEvent @event, CancellationToken cancellationToken = default)
    {
        
        using var producer = new ProducerBuilder<Null, IEvent>(new ProducerConfig()
        {
            BootstrapServers = _kafkaConfig.Value.BootstrapServers,
        })
        .SetValueSerializer(new Test<IEvent>())
            .Build();
        
        foreach (var topic in topics)
        {
            await producer.ProduceAsync(topic, new Message<Null, IEvent>() {Value = @event}, cancellationToken);
            producer.Flush(TimeSpan.FromSeconds(10));
        }
        
        
    }
}
