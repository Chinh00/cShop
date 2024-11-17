using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using MediatR;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Cdc;

public class BackgroundConsumerService(
    ILogger<BackgroundConsumerService> logger, 
    IOptions<ConsumerConfig> config,
    IServiceScopeFactory scopeFactory) : BackgroundService
{

    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Factory.StartNew(() => KafkaConsumer(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

    private async Task KafkaConsumer(CancellationToken stoppingToken)
    {
        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        using var schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig()
        {
            Url = config.Value.SchemaRegistryServer
        });
        var consumerBuilder = new ConsumerBuilder<string, GenericRecord>(config.Value)
            .SetErrorHandler((_, e) => logger.LogError($"Error: {e.Reason}"))
            .SetStatisticsHandler((_, json) => logger.LogInformation($"Statistics: {json}"))
            .SetValueDeserializer(new AvroDeserializer<GenericRecord>(schemaRegistryClient).AsSyncOverAsync())
            .Build();
        consumerBuilder.Subscribe(config.Value.TopicName);
        try
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                logger.LogInformation("Starting consumer...");

                try
                {
                    var result = consumerBuilder.Consume();
                    if (result is null) continue;
                    var eventName = result.Message.Value.Schema?.Name;
                    
                    logger.LogInformation(result.Message.Value.ToString());
                    var bytes = await (new AvroSerializer<GenericRecord>(schemaRegistryClient, new AvroSerializerConfig()
                    {
                        SubjectNameStrategy = SubjectNameStrategy.Topic
                    })).SerializeAsync(result.Message.Value, new SerializationContext(MessageComponentType.Value, $"{config.Value.TopicName}-anchor"));
                    var res = await config.Value.HandlePayload(schemaRegistryClient, eventName, bytes);

                    if (res is INotification)
                    {
                        logger.LogInformation("Kafka message received");
                        await mediator.Publish(res, stoppingToken);
                    }

                    consumerBuilder.Commit(result);
                }
                catch (Exception e)
                {
                    logger.LogInformation(e.Message);
                }

                
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
        finally
        {
            consumerBuilder.Dispose();
        }
    }
    
}