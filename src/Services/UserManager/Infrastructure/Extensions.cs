using Confluent.Kafka;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddMassTransit(c =>
        {
            c.SetKebabCaseEndpointNameFormatter();
            c.UsingInMemory();
            c.AddRider(t =>
            {
                t.AddConsumer<EventDispatcher>();
                t.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    configurator.TopicEndpoint<UserCreatedIntegrationEvent>(nameof(UserCreatedIntegrationEvent), "user-customer-group",
                        endpointConfigurator =>
                        {
                            endpointConfigurator.CreateIfMissing(e => e.NumPartitions = 1);
                            endpointConfigurator.ConfigureConsumer<EventDispatcher>(context);
                            endpointConfigurator.AutoOffsetReset = AutoOffsetReset.Earliest;
                        });
                });
            });
        });
        
        
        action?.Invoke(services);
        return services;
    }
}