
using Application;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.SchemaRegistry;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "Notification")
    .AddMediatorDefault([typeof(Program), typeof(Infrastructure.Extensions), typeof(Anchor)])
    .AddSchemaRegistry(builder.Configuration)
    .AddCdcConsumer(builder.Configuration);


var app = builder.Build();



app.Run();
