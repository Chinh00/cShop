using Polly;
namespace Identity.Api.Data;

public class DbMigrationService(IServiceProvider serviceProvider, ILogger<DbMigrationService> logger) : IHostedService
{

    public async Task StartAsync(CancellationToken cancellationToken)
        => await PollyPolicy(5).ExecuteAsync(async () =>
        {
            Console.WriteLine("Database Migration Service is starting.");
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
        });

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    IAsyncPolicy PollyPolicy(int retryCount) => Policy.Handle<Exception>().WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
        (exception, timeSpan, rCount, context) =>
        {
            logger.LogError(exception.Message);
        });
}