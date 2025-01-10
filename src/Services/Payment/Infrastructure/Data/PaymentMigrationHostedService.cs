using cShop.Infrastructure.Data;

namespace Infrastructure.Data;

public class PaymentMigrationHostedService(
    IServiceProvider serviceProvider,
    ILogger<MigrationHostedService<PaymentContext>> logger)
    : MigrationHostedService<PaymentContext>(serviceProvider, logger)
{
    
}