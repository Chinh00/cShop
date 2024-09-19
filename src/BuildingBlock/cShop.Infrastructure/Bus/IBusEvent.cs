

using cShop.Core.Domain;

namespace cShop.Infrastructure.Bus;

public interface IBusEvent
{
    Task PublishAsync(string [] topics, IEvent @event, CancellationToken cancellationToken = default);
}