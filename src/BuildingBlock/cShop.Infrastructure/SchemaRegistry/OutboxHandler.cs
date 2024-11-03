
using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using cShop.Core.Domain;
using cShop.Core.Repository;

namespace cShop.Infrastructure.SchemaRegistry;

public class OutboxHandler<TOutbox>(ISchemaRegistryClient schemaRegistryClient, IRepository<TOutbox> repository)
    where TOutbox : OutboxEntity

{

    public async ValueTask SendToOutboxAsync<TAggregate, TEvent>(
        TAggregate aggregateRoot,
        Func<(TOutbox, TEvent, string)> eventFunc, CancellationToken cancellationToken = default)    
        where TEvent : ISpecificRecord
        where TAggregate : AggregateBase
    {

        var (outbox, @event, topicName) = eventFunc();
        
        var avroDeserializeConfig = new AvroSerializerConfig()
        {
            SubjectNameStrategy = SubjectNameStrategy.Topic
        };

        var deserizalize = new AvroSerializer<ISpecificRecord>(schemaRegistryClient, avroDeserializeConfig);
        var bytes = await deserizalize.SerializeAsync(@event, new SerializationContext(MessageComponentType.Value, topicName));

        outbox.Id = Guid.NewGuid();
        outbox.AggregateType = typeof(TAggregate).Name;
        outbox.AggregateId = aggregateRoot.Id.ToString();
        outbox.Type = nameof(TEvent);
        outbox.Payload = bytes;
        

        
        await repository.AddAsync(outbox, cancellationToken);


    }
    
}