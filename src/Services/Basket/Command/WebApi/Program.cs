using Application;
using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Logging;
using cShop.Infrastructure.Mediator;
using cShop.Infrastructure.Swagger;
using WebApi.Application;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddLoggingCustom(builder.Configuration, "Basket - Command")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    ;


var app = builder.Build();

app.UseAuthenticationDefault(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

app.Run();

