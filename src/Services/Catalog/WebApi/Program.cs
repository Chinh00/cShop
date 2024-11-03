

using Application;
using Bus;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using GrpcService.Implements;
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLoggingCustom(builder.Configuration, "Catalog")
    .AddOpenTelemetryCustom("CatalogService")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddDbContextService(builder.Configuration)
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration)
    .AddGrpc();


var app = builder.Build();

app.NewVersionedApi("Catalog").MapCatalogApiV1();
app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration)
    .MapGrpcService<CatalogGrpcService>()
    ;




app.Run();

