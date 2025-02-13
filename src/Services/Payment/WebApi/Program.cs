using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Payment service")
    .AddOpenTelemetryCustom(builder.Configuration, "payment-service")
    .AddAuthenticationDefault(builder.Configuration)
    .AddValidation(typeof(Anchor))
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Anchor)])
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddDbContextService(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration)
    .AddPaymentService(builder.Configuration);


var app = builder.Build();



app.NewVersionedApi("Payment").MapPaymentApiV1();

app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);


app.Run();

