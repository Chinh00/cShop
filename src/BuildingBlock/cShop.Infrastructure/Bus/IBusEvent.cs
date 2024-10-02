

using cShop.Core.Domain;

namespace cShop.Infrastructure.Bus;

public interface IBusEvent
{
    public Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : Message;
}