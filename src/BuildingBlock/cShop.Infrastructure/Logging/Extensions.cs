using Serilog;

namespace cShop.Infrastructure.Logging;

public static class Extensions
{
    public static IServiceCollection AddLoggingCustom(this IServiceCollection services, IConfiguration configuration)
    {
        
        var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();    
        
        Log.Logger = logger;
        services.AddSerilog();
        return services;
    }
}