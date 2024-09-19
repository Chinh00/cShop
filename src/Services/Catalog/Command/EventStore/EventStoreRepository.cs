using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreDbContext _context;

    public EventStoreRepository(EventStoreDbContext context)
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