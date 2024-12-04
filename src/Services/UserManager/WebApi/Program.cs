using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.SchemaRegistry;
using cShop.Infrastructure.Swagger;
using cShop.Infrastructure.Validation;
using Infrastructure.Data;
using WebApi.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "UserService")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddValidation(typeof(Anchor))
    .AddMediatorDefault([typeof(Anchor)])
    .AddRepository(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration);
    


var app = builder.Build();


app.UseAuthenticationDefault(builder.Configuration)
    .NewVersionedApi("User").MapUserApi();
app.ConfigureSwagger(builder.Configuration);

app.Run();

