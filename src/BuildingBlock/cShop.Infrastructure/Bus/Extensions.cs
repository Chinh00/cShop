
using cShop.Infrastructure.Bus.Masstransit;

namespace cShop.Infrastructure.Bus;

public static class Extensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddScoped<IBusEvent, MasstransitBusEvent>();
        
        action?.Invoke(services);
        return services;
    }

    
}