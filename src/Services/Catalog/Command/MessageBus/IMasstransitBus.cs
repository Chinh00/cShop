using cShop.Core.Domain;

namespace MessageBus;

public interface IMasstransitBus
{
    public Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : Message;
}