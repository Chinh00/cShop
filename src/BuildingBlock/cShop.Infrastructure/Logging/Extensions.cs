using Serilog;

namespace cShop.Infrastructure.Logging;

public static class Extensions
{
   
    public static IServiceCollection AddLoggingCustom(this IServiceCollection services, IConfiguration configuration, string applicationName,
        Action<IServiceCollection>? action = null)
    {

        var logger = new LoggerConfiguration().Enrich.FromLogContext().Enrich.WithProperty("Application Name", applicationName).WriteTo.Console().CreateLogger();    
        
        Log.Logger = logger;
        services.AddSerilog();
        
        action?.Invoke(services);
        return services;
    }
}