using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class ShipperMigrationHosted(IServiceProvider serviceProvider) : MigrationHostedService<ShipperContext>(serviceProvider)
{
    
}