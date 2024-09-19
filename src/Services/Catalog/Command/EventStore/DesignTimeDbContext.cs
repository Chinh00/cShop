using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventStore;

public class DesignTimeDbContext : IDesignTimeDbContextFactory<EventStoreDbContext>
{
    public EventStoreDbContext CreateDbContext(string[] args)
    {
        var option = new DbContextOptionsBuilder().UseSqlServer("Server=localhost;Database=EventStore;Encrypt=false;User Id=sa;Password=@P@ssw0rd02");
        return (EventStoreDbContext)Activator.CreateInstance(typeof(EventStoreDbContext), option.Options);
    }
}