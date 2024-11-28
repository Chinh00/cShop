using cShop.Infrastructure.SchemaRegistry;
using Identity.Api;
using Identity.Api.Data;
using IntegrationEvents;
using MassTransit;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddHostedService<DbMigrationService>();

    builder.Services.AddMassTransit(e =>
    {
        e.SetKebabCaseEndpointNameFormatter();
        e.UsingInMemory();
        e.AddRider(t =>
        {
            
            //t.AddProducer<UserCreatedIntegrationEvent>(nameof(UserCreatedIntegrationEvent));
            t.UsingKafka((context, configurator) =>
            {
                configurator.Host(builder.Configuration.GetValue<string>("Kafka:BootstrapServers"));
            });
        });
    });
    

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}