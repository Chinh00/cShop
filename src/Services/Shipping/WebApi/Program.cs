using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Mongodb;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure;
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "ShipperService")
    .AddValidation(typeof(Anchor))
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom()
    .AddMediatorDefault([typeof(Anchor), typeof(Program)])
    .AddRepository(builder.Configuration)
    .AddMasstransits(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration)
    .AddCdcConsumers(builder.Configuration)
    .AddMongoDb(builder.Configuration);




var app = builder.Build();


app.UseAuthenticationDefault(app.Configuration);
app.NewVersionedApi("Shipper").MapShipperApi();
app.ConfigureSwagger(app.Configuration);

app.Run();

