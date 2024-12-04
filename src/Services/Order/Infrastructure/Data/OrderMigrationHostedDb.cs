using cShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderMigrationHostedDb(IServiceProvider serviceProvider, ILogger<OrderMigrationHostedDb> logger) : MigrationHostedService<OrderContext>(serviceProvider, logger)
{
    public override async Task DoMoreAction(OrderContext dbContext, CancellationToken cancellationToken)
    {
        var count = await dbContext.Database.SqlQuery<int>($"select count(1) as Value from sys.tables where name = 'OrderOutboxes' AND is_tracked_by_cdc = 'TRUE'"
        ).FirstOrDefaultAsync(cancellationToken) == 0;
        
        if (count) await dbContext.Database.ExecuteSqlAsync($"EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'OrderOutboxes', @role_name = NULL;", cancellationToken: cancellationToken);
        await base.DoMoreAction(dbContext, cancellationToken);
    }
}