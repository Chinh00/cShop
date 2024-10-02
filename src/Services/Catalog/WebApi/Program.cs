using Application;
using Bus;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Swagger;
using EventStore;
using GrpcService.Implements;
using Projection;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "Catalog")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddEventStoreCustom(builder.Configuration)
    .AddProjectionCustom(builder.Configuration)
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddGrpc();


var app = builder.Build();

app.NewVersionedApi("Catalog").MapCatalogApiV1();
app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration)
    .MapGrpcService<CatalogGrpcService>()
    ;




app.Run();

