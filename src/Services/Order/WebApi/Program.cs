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
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "Order")
    .AddOpenTelemetryCustom(builder.Configuration, "order-service")
    .AddValidation(typeof(Anchor))
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddMessageBus(builder.Configuration)
    .AddMasstransitCustom(builder.Configuration)
    .AddDbContextService(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration)
    .AddCdcConsumer()
    ;
var app = builder.Build();

app.NewVersionedApi("Order").MapOrderV1Api();

app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

app.Run();
