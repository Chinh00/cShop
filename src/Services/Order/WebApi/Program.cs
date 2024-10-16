using Application;
using Bus;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.Swagger;
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Order")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddDbContextCustom(builder.Configuration)
    .AddOpenTelemetryCustom("Order")
    ;

var app = builder.Build();

app.NewVersionedApi("Order").MapOrderV1Api();
app.NewVersionedApi("Payment").MapPaymentApiV1();

app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

app.Run();
