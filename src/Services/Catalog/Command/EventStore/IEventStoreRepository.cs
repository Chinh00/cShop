using cShop.Contracts.Abstractions;
using cShop.Core.Domain;

namespace EventStore;

public interface IEventStoreRepository
{
    public Task AppendEventAsync(StoreEvent @event, CancellationToken cancellationToken);

    public Task<TEntity> LoadAggregateEventsAsync<TEntity>(Guid aggregateId, CancellationToken cancellationToken) where TEntity : AggregateBase, new();
}