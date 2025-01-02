var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration, "UserService")
    .AddAuthenticationDefault(builder.Configuration)
    .AddSwaggerCustom(builder.Configuration)
    .AddValidation(typeof(Anchor))
    .AddMediatorDefault([typeof(Anchor)])
    .AddAutoMapper(typeof(UserConfigMapper))
    .AddRepository(builder.Configuration)
    .AddSchemaRegistry(builder.Configuration);
    


var app = builder.Build();


app.UseAuthenticationDefault(builder.Configuration)
    .NewVersionedApi("User").MapUserApi();
app.ConfigureSwagger(builder.Configuration);

app.Run();

