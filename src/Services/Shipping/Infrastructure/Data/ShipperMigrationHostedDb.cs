using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class ShipperMigrationHostedDb(IServiceProvider serviceProvider, ILogger<ShipperMigrationHostedDb> logger) : MigrationHostedService<ShipperContext>(serviceProvider, logger)
{
    
}