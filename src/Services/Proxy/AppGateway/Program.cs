using cShop.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLoggingCustom(builder.Configuration, "ReverseProxy");

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();


app.UseRouting();
app.MapReverseProxy();

app.Run();
