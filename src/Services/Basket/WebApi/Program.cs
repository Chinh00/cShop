using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Cache;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Basket")
    .AddOpenTelemetryCustom(builder.Configuration, "basket-service")
    .AddValidation(typeof(Anchor))
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddRedisCache(builder.Configuration)
    .AddCatalogGrpcClient(builder.Configuration)
    .AddCustomMasstransit(builder.Configuration);
var app = builder.Build();


app.NewVersionedApi("Basket").MapBasketApiV1();
app.UseAuthenticationDefault()
    .ConfigureSwagger(builder.Configuration);

app.Run();
