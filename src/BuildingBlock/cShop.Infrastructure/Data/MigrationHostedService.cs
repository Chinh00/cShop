using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.Data;

public class MigrationHostedService<TDbContext>(IServiceProvider serviceProvider): IHostedService
    where TDbContext : DbContext
{
    
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        await dbContext.Database.MigrateAsync(cancellationToken);


    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}