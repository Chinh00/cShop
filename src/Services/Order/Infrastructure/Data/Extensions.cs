using cShop.Core.Repository;
using cShop.Infrastructure.Data;
using Infrastructure.Data.Internal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddDbContextCustom<OrderContext>(configuration, typeof(OrderContext));
        //services.AddHostedService<DbMigrationHostedService>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        action?.Invoke(services);
        return services;
    }
}