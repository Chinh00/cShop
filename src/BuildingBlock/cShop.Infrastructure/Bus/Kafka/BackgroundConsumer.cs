
using Confluent.Kafka;
using cShop.Core.Domain;
using MediatR;
using Microsoft.Extensions.Options;

namespace cShop.Infrastructure.Bus.Kafka;

public class BackgroundConsumer : BackgroundService
{
    private readonly ILogger<BackgroundConsumer> _logger;

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IOptions<KafkaConfig> _kafkaConfig;
    private readonly KafkaConsumerConfig _consumerConfig;
    public BackgroundConsumer(ILogger<BackgroundConsumer> logger, IServiceScopeFactory serviceScopeFactory, IOptions<KafkaConfig> kafkaConfig, KafkaConsumerConfig consumerConfig)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _kafkaConfig = kafkaConfig;
        _consumerConfig = consumerConfig;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Factory.StartNew(() => ConsumeTopic(stoppingToken),
            stoppingToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Current);
    }

    private async Task ConsumeTopic(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        using var consumer = new ConsumerBuilder<Null, IEvent>(new ConsumerConfig()
            {
                BootstrapServers = _kafkaConfig.Value.BootstrapServers
            })
            .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
            .SetStatisticsHandler((_, json) => _logger.LogInformation($"Statistics: {json}"))
            .SetValueDeserializer(new Test1<IEvent>())
            .Build();

        consumer.Subscribe(_consumerConfig.TopicName);

        try
        {
            while (stoppingToken.IsCancellationRequested) 
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    if (result is null ) continue;
                    
                    
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}