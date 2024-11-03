using cShop.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace cShop.Infrastructure.Data;

public static class Extensions
{
    public static IServiceCollection AddDbContextCustom<TDbContext>(this IServiceCollection services, IConfiguration configuration,
        Type repoType,
        Action<IServiceCollection>? action = null)
        where TDbContext : DbContext
    {

        services.AddDbContext<TDbContext>((provider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("Db"));
        });

        
        
        action?.Invoke(services);
        return services;
    }
}