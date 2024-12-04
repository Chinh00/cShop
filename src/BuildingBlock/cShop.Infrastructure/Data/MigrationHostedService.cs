using Microsoft.EntityFrameworkCore;
using Polly;

namespace cShop.Infrastructure.Data;

public class MigrationHostedService<TDbContext>(IServiceProvider serviceProvider, ILogger<MigrationHostedService<TDbContext>> logger): IHostedService
    where TDbContext : DbContext
{
        
    
    public virtual Task DoMoreAction(TDbContext dbContext, CancellationToken cancellationToken) => Task.CompletedTask;
     

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        

        await PollyPolicy(5).ExecuteAsync(async () =>
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            if ((await dbContext.Database.GetPendingMigrationsAsync(cancellationToken: cancellationToken)).Any())
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
            //await dbContext.Database.MigrateAsync(cancellationToken);
            if (dbContext.Database.IsSqlServer())
            {
                //enable database change data capture
                await dbContext.Database.ExecuteSqlAsync($@"EXEC sys.sp_cdc_enable_db", cancellationToken: cancellationToken);
                await DoMoreAction(dbContext, cancellationToken);
            }
        });

    }

     IAsyncPolicy PollyPolicy(int retryCount) => Policy.Handle<Exception>().WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
        (exception, timeSpan, rCount, context) =>
        {
            logger.LogError(exception.Message);
        });

    
    
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}