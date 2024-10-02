using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.EventStore;

public class EventStoreRepositoryBase<TDbContext> : IEventStoreRepository
    where TDbContext : EventStoreDbContextBase
{
    private readonly TDbContext _context;

    public EventStoreRepositoryBase(TDbContext context)
    {
        _context = context;
    }

    public async Task AppendEventAsync(StoreEvent @event, CancellationToken cancellationToken)
    {
        await _context.Set<StoreEvent>().AddAsync(@event, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> LoadAggregateEventsAsync<TEntity>(Guid aggregateId, CancellationToken cancellationToken)
        where TEntity : AggregateBase, new()
    {
        var listEvents = await _context.Set<StoreEvent>().Where(e => e.AggregateId == aggregateId).ToListAsync(cancellationToken: cancellationToken);
        
        
        var aggregate = new TEntity();
        
        aggregate.ApplyEvents(listEvents.Select(e => e.Event).ToList());

        return aggregate;
    }
}