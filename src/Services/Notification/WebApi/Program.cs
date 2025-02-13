
using Application;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Ole;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using Infrastructure;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "Notification")
    .AddOpenTelemetryCustom(builder.Configuration, "notification-service")
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Program), typeof(Infrastructure.Extensions), typeof(Anchor)])
    .AddSchemaRegistry(builder.Configuration)
    .AddCdcConsumer(builder.Configuration);

var app = builder.Build();

app.NewVersionedApi("Mail").MapMailApiV1();
app.ConfigureSwagger(builder.Configuration);

app.Run();
