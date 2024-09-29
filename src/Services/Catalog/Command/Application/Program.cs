using cShop.Infrastructure.Auth;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.IdentityServer;
using cShop.Infrastructure.Logging;
using EventStore;
using MessageBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});


builder.Services
    .AddLoggingCustom(builder.Configuration, "Catalog - Command")
    .AddAuthenticationDefault(builder.Configuration)
    .AddEventStore(builder.Configuration)
    .AddCustomMasstransit(builder.Configuration)
    
    .AddMediatR(e => e.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddControllers();

builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Cors");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

