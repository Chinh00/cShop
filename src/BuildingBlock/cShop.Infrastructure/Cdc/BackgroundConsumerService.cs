using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Cdc;

public class BackgroundConsumerService(
    ILogger<BackgroundConsumerService> logger, 
    ISchemaRegistryClient schemaRegistryClient, 
    IOptions<ConsumerConfig> config,
    IServiceScopeFactory scopeFactory) : BackgroundService
{

    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Factory.StartNew(() => KafkaConsumer(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

    private async Task KafkaConsumer(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        
        var consumerBuilder = new ConsumerBuilder<string, GenericRecord>(config.Value)
            .SetValueDeserializer(new AvroDeserializer<GenericRecord>(schemaRegistryClient).AsSyncOverAsync())
            .Build();
        consumerBuilder.Subscribe(config.Value.TopicName);
        try
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                var result = consumerBuilder.Consume();

                if (result is null) continue;
                var eventName = result.Message.Value.Schema?.Name;
                var bytes = await (new AvroSerializer<GenericRecord>(schemaRegistryClient)).SerializeAsync(result.Message.Value, new SerializationContext());
                var res = await config.Value.HandlePayload(schemaRegistryClient, eventName, bytes);


                if (res is INotification)
                {
                    logger.LogInformation("Kafka message received");
                    await mediator.Publish(res, stoppingToken);
                }


                consumerBuilder.Commit(result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            consumerBuilder.Dispose();
        }
    }
    
}