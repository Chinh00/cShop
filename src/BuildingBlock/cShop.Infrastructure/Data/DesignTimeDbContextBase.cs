using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace cShop.Infrastructure.Data;

public class DesignTimeDbContextBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : AppBaseContext
{
  

    public TDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
            .AddEnvironmentVariables()
            .Build();
        
        var conn = configuration.GetConnectionString("EventStore");
        Console.WriteLine("Server=localhost;Database=EventStore;Encrypt=false;User Id=sa;Password=@P@ssw0rd02");
        var options = new DbContextOptionsBuilder<TDbContext>().UseSqlServer("Server=localhost;Database=Db;Encrypt=false;User Id=sa;Password=@P@ssw0rd02", builder =>
        {
            builder.EnableRetryOnFailure();
        });

        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), options.Options);
    }
}