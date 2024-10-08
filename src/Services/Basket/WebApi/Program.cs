using Application;
using Bus;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.Swagger;
using EventStore;
using Infrastructure;
using Projection;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Basket")
    .AddOpenTelemetryCustom("BasketService")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddEventStoreCustom(builder.Configuration)
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddProjectionCustom(builder.Configuration)
    .AddCatalogGrpcClient(builder.Configuration)
    ;


var app = builder.Build();


app.NewVersionedApi("Basket").MapBasketApiV1();
app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

app.Run();
