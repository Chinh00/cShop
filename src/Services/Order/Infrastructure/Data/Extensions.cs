using Infrastructure.Data.Internal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContextCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddDbContext<OrderContext>(e =>
        {
            e.UseSqlServer(configuration.GetConnectionString("OrderContext"));
        });
        services.AddHostedService<DbMigrationHostedService>();
        
        action?.Invoke(services);
        return services;
    }
}