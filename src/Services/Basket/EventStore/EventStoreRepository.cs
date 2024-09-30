using cShop.Infrastructure.EventStore;

namespace EventStore;

public class EventStoreRepository(BasketEventStoreDbContext context)
    : EventStoreRepositoryBase<BasketEventStoreDbContext>(context);