using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.ServiceDiscovery;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using GrpcService.Implements;
using Infrastructure;
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLoggingCustom(builder.Configuration, "Catalog")
    .AddOpenTelemetryCustom(builder.Configuration,"catalog-service")
    .AddValidation(typeof(Anchor))
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddDbContextService(builder.Configuration)
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration)
    .AddGrpc();






var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.NewVersionedApi("Catalog").MapCatalogApiV1();
app.MapGet("/health", context => context.Response.WriteAsync("OK"));
app.UseAuthenticationDefault()
    .ConfigureSwagger(builder.Configuration)
    .MapGrpcService<CatalogGrpcService>();
app.UseAntiforgery();




app.Run();

