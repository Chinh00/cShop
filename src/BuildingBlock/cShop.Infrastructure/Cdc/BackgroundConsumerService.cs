using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using MediatR;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Cdc;

public class BackgroundConsumerService<TConfig> : BackgroundService 
    where TConfig : BackgroundConsumerConfig
{
    private readonly ILogger<BackgroundConsumerService<TConfig>> _logger;
    private readonly BackgroundConsumerConfig _config;
    private readonly IServiceScopeFactory _scopeFactory;

    public BackgroundConsumerService(ILogger<BackgroundConsumerService<TConfig>> logger, IServiceScopeFactory scopeFactory, IOptions<TConfig> config)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _config = config.Value;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Factory.StartNew(() => KafkaConsumer(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);

    private async Task KafkaConsumer(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        using var schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig()
        {
            Url = _config.SchemaRegistryServer
        });
        var consumerBuilder = new ConsumerBuilder<string, GenericRecord>(_config)
            .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
            .SetStatisticsHandler((_, json) => _logger.LogInformation($"Statistics: {json}"))
            .SetValueDeserializer(new AvroDeserializer<GenericRecord>(schemaRegistryClient).AsSyncOverAsync())
            .Build();
        consumerBuilder.Subscribe(_config.Topic);
        
        try
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    var result = consumerBuilder.Consume();
                    if (result is null) continue;
                    var eventName = result.Message.Value.Schema?.Name;
                    Console.WriteLine(eventName);

                    var bytes = await (new AvroSerializer<GenericRecord>(schemaRegistryClient, new AvroSerializerConfig()
                    {
                        SubjectNameStrategy = SubjectNameStrategy.Topic
                    })).SerializeAsync(result.Message.Value, new SerializationContext(MessageComponentType.Value, $"{_config.Topic}-anchor"));
                    var res = await _config.HandlePayload(schemaRegistryClient, eventName, bytes);

                    
                    if (res is INotification)
                    {
                        await mediator.Publish(res, stoppingToken);
                    }

                    consumerBuilder.Commit(result);
                }
                catch (ConsumeException e)
                {
                    _logger.LogInformation(e.Message);
                }

                
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError(e.Message);
            consumerBuilder.Close();
        }
        
    }
    
}