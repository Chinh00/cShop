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

builder.Services.AddLoggingCustom(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEventStore(builder.Configuration);



builder.Services.AddCustomMasstransit(builder.Configuration);
builder.Services.AddMessageBus(builder.Configuration);





builder.Services.AddSwaggerGen();





builder.Services.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();

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

