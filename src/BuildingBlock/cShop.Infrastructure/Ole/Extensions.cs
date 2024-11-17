using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace cShop.Infrastructure.Ole;

public static class Extensions
{
    public static IServiceCollection AddOpenTelemetryCustom(this IServiceCollection services, string appName,
        Action<IServiceCollection>? action = null)
    {


        services.AddOpenTelemetry()
                     .WithTracing(tracerProviderBuilder =>
                     {
                         tracerProviderBuilder
                             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appName))
                             .AddAspNetCoreInstrumentation()
                             .AddHttpClientInstrumentation()
                             .AddSource("MassTransit")
                             .AddJaegerExporter(options =>
                             {
                                 options.AgentHost = "localhost";
                                 options.AgentPort = 6831;
                             });
                     });

        action?.Invoke(services);
        return services;
    }
}