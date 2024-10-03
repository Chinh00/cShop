using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Internal;

public class DbMigrationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DbMigrationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}