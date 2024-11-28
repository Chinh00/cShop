
using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class UserMigrationHostedDb(IServiceProvider serviceProvider) : MigrationHostedService<UserContext>(serviceProvider)
{
    
}