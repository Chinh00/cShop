using cShop.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class CatalogEventStoreDbContext(DbContextOptions options)
    : EventStoreDbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogEventStoreDbContext).Assembly);
    }
}