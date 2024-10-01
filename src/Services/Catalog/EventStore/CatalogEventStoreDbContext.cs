using cShop.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class CatalogEventStoreDbContext(DbContextOptions options) : EventStoreDbContextBase(options)
{
    
}