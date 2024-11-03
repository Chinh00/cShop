using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Cache;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.Swagger;
using Infrastructure;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Basket")
    .AddOpenTelemetryCustom("BasketService")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddRedisCache(builder.Configuration)
    .AddCatalogGrpcClient(builder.Configuration)
    .AddCustomMasstransit(builder.Configuration)
    ;
var app = builder.Build();


app.NewVersionedApi("Basket").MapBasketApiV1();
app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

app.Run();
