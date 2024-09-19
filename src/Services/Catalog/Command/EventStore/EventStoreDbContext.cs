
using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventStoreDbContext).Assembly);
    }
}