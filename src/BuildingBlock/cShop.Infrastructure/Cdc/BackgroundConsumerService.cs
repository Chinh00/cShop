using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using MediatR;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Cdc;

public class BackgroundConsumerService : BackgroundService
{
    private readonly ILogger<BackgroundConsumerService> _logger;
    private readonly BackgroundConsumerConfig _config;
    private readonly IServiceScopeFactory _scopeFactory;

    public BackgroundConsumerService(ILogger<BackgroundConsumerService> logger, IServiceScopeFactory scopeFactory, IOptions<BackgroundConsumerConfig> config)
    {
        this._logger = logger;
        this._scopeFactory = scopeFactory;
        this._config = config.Value;
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
        _logger.LogInformation(_config.Topic);
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
                    
                    var bytes = await (new AvroSerializer<GenericRecord>(schemaRegistryClient, new AvroSerializerConfig()
                    {
                        SubjectNameStrategy = SubjectNameStrategy.Topic
                    })).SerializeAsync(result.Message.Value, new SerializationContext(MessageComponentType.Value, $"{_config.Topic}-anchor"));
                    var res = await _config.HandlePayload(schemaRegistryClient, eventName, bytes);

                    
                    _logger.LogInformation($"Handler message {result}");
                    if (res is INotification)
                    {
                        _logger.LogInformation("Kafka message received");
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