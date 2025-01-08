using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data;

public class DbMigrationService(IServiceProvider serviceProvider) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Database Migration Service is starting.");
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}