using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EventStore;

public static class Extensions
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection>? action = null)
    {
        
        services.AddDbContext<EventStoreDbContext>((provider, builder) =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("EventStore"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });

        services.AddHostedService<EventStoreHostedService>();
        
        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped(e => e.GetService<DatabaseFacade>());
        
        
        action?.Invoke(services);   
        return services;
    }
}