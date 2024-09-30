using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.EventStore;

public class EventStoreDbContextBase(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContextBase).Assembly);
    }
}