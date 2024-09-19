

using cShop.Infrastructure.Logging;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLoggingCustom(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(e => e.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddGrpcClientCustom(builder.Configuration);

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();

