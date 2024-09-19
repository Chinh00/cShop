using System.Net;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.Logging;
using EventBus;
using EventBus.Consumers;
using GrpcService.Implements;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Projections;
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddLoggingCustom(builder.Configuration);

builder.Services.AddProjections(builder.Configuration);

builder.Services.AddConsumerCustom(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 3000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();

    });
});


builder.Services.AddMediatR(e => e.RegisterServicesFromAssemblies([typeof(ProductCreatedDomainEventConsumer).Assembly, typeof(Program).Assembly]));

builder.Services.AddCustomMasstransit(builder.Configuration).AddEventBus(builder.Configuration);


var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGrpcService<CatalogGrpcService>();

app.Run();

