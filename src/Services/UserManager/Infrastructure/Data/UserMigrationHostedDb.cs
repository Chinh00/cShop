
using cShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserMigrationHostedDb(IServiceProvider serviceProvider, ILogger<UserMigrationHostedDb> logger) : MigrationHostedService<UserContext>(serviceProvider, logger)
{
    public override async Task DoMoreAction(UserContext dbContext, CancellationToken cancellationToken)
    {
        var countCustomer = await dbContext.Database.SqlQuery<int>($"select count(1) as Value from sys.tables where name = 'CustomerOutboxes' AND is_tracked_by_cdc = 'TRUE'"
        ).FirstOrDefaultAsync(cancellationToken) == 0;
        var countShipper = await dbContext.Database.SqlQuery<int>($"select count(1) as Value from sys.tables where name = 'ShipperOutboxes' AND is_tracked_by_cdc = 'TRUE'"
        ).FirstOrDefaultAsync(cancellationToken) == 0;
        
        if (countCustomer) await dbContext.Database.ExecuteSqlAsync($"EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'CustomerOutboxes', @role_name = NULL;", cancellationToken: cancellationToken);
        if (countShipper) await dbContext.Database.ExecuteSqlAsync($"EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'ShipperOutboxes', @role_name = NULL;", cancellationToken: cancellationToken);
        await base.DoMoreAction(dbContext, cancellationToken);
    }
}