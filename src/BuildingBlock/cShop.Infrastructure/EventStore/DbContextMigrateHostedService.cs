using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.EventStore;

public class DbContextMigrateHostedService<TDbContext> : IHostedService
    where TDbContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;

    public DbContextMigrateHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        if (dbContext is not null)
        {
            await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}