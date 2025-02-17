using Consul;

namespace cShop.Infrastructure.ServiceDiscovery;

public class ConsulStartedService(IServiceScopeFactory scopeFactory, IConfiguration configuration) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var consulClient = scope.ServiceProvider.GetRequiredService<IConsulClient>();
        
        var serviceName = configuration["Consul:ServiceName"] ?? "test-service";
        var serviceId = $"{serviceName}-{Guid.NewGuid()}";
        var serviceAddress = configuration["Consul:ServiceAddress"] ?? "http://localhost:5289";
        var uri = new Uri(serviceAddress);

        var registration = new AgentServiceRegistration()
        {
            ID = serviceId,
            Name = serviceName,
            Address = uri.Host,
            Port = uri.Port,
            Check = new AgentServiceCheck()
            {
                HTTP = $"{serviceAddress}/health",
                Interval = TimeSpan.FromSeconds(3),
                Timeout = TimeSpan.FromSeconds(5),
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
            }
        };

        consulClient.Agent.ServiceRegister(registration, cancellationToken).GetAwaiter().GetResult();
        
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}