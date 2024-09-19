using cShop.Core.Domain;
using MassTransit;

namespace MessageBus;

public class MasstransitBus : IMasstransitBus
{
    
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MasstransitBus(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : Message
    {
        var producer = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<ITopicProducer<TMessage>>();


        await producer.Produce(message, cancellationToken);

    }
}