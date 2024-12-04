using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace cShop.Infrastructure.Data;

public class DesignTimeDbContextBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : AppBaseContext
{
  

    public TDbContext CreateDbContext(string[] args)
    {
        
        Console.WriteLine("Server=localhost;Database=EventStore;Encrypt=false;User Id=sa;Password=@P@ssw0rd02");
        var options = new DbContextOptionsBuilder<TDbContext>().UseSqlServer("Server=localhost;Database=Db;Encrypt=false;User Id=sa;Password=@P@ssw0rd02", builder =>
        {
            builder.EnableRetryOnFailure();
        });

        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options.Options);
    }
}