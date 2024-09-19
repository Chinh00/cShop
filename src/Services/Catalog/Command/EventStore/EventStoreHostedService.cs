using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class EventStoreHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public EventStoreHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<EventStoreDbContext>().Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}