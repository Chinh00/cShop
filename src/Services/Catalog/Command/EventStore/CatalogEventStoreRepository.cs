using cShop.Infrastructure.EventStore;

namespace EventStore;

public class CatalogEventStoreRepository(CatalogEventStoreDbContext context)
    : EventStoreRepositoryBase<CatalogEventStoreDbContext>(context);