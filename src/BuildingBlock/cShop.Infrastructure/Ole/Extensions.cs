using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace cShop.Infrastructure.Ole;

public static class Extensions
{
    public static IServiceCollection AddOpenTelemetryCustom(this IServiceCollection services, IConfiguration config, string appName,
        Action<IServiceCollection>? action = null)
    {

        services.AddHttpClient();
        services.AddOpenTelemetry()
            .WithMetrics(c =>
            {
                c.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appName))
                    .AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri($"{config.GetValue<string>("Oltp:Endpoint")}");                        
                        // options.Endpoint = new Uri("http://localhost:4321");
                        options.Protocol = OtlpExportProtocol.Grpc;
                    })
                    // .AddConsoleExporter()
                    ;

            })
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appName))
                    .SetSampler(new AlwaysOnSampler())
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource("MassTransit")
                    .AddSqlClientInstrumentation(c =>
                    {
                        c.SetDbStatementForText = true;
                    })
                    .AddEntityFrameworkCoreInstrumentation(e =>
                    {
                        e.SetDbStatementForText = true;
                    })
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri($"{config.GetValue<string>("Jaeger:Protocol")}://{config.GetValue<string>("Jaeger:Host")}:{config.GetValue<string>("Jaeger:Port")}");
                    });
            });

        action?.Invoke(services);
        return services;
    }
}