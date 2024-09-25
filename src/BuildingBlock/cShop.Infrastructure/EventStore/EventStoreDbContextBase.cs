using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.EventStore;

public class EventStoreDbContextBase : DbContext
{
    public EventStoreDbContextBase(DbContextOptions options) : base(options)
    {
        
    }
    
    
    
}