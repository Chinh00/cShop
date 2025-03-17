using cShop.Infrastructure.Ole;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "UserService")
    .AddOpenTelemetryCustom(builder.Configuration, "user-service")
    .AddMasstransitService(builder.Configuration)
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom()
    .AddValidation(typeof(Anchor))
    .AddMediatorDefault([typeof(Program), typeof(Anchor)])
    .AddAutoMapper(typeof(UserConfigMapper))
    .AddRepository(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration);
    


var app = builder.Build();


app.UseAuthenticationDefault()
    .NewVersionedApi("User").MapUserApi();
app.ConfigureSwagger(builder.Configuration);

app.Run();

