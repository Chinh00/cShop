using cShop.Core.Domain;
using MassTransit;

namespace cShop.Infrastructure.Bus.Masstransit;

public class MasstransitBusEvent(IServiceScopeFactory serviceScopeFactory, ILogger<MasstransitBusEvent> logger)
    : IBusEvent
{
    private readonly ILogger<MasstransitBusEvent> _logger = logger;

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : Message
    {
        var producer = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITopicProducer<TMessage>>();
        
        _logger.LogInformation("Publishing message: {@Message}", message);
        
        await producer.Produce(message, cancellationToken);
    }
}