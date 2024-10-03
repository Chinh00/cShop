using Application;
using Bus;
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
