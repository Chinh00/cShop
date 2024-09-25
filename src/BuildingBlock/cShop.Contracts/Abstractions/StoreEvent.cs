using cShop.Core.Domain;

namespace cShop.Contracts.Abstractions;


public sealed record StoreEvent(Guid AggregateId, string AggregateType, string EventType, IDomainEvent @Event, long Version, DateTime CreatedAt)
{
    public static StoreEvent Create(AggregateBase aggregateBase, IDomainEvent @event) =>
        new StoreEvent(aggregateBase.Id, aggregateBase.GetType().Name, @event.GetType().Name, @event, @event.Version, @event.CreateAt);
}