using Confluent.Kafka;
using cShop.Infrastructure.Data;
using Infrastructure.Data;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddMasstransitCustom(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddMassTransit(r =>
        {
            r.SetKebabCaseEndpointNameFormatter();
            r.UsingInMemory();
            r.AddRider(c =>
            {
                c.AddProducer<PaymentProcessSuccessIntegrationEvent>(nameof(PaymentProcessSuccessIntegrationEvent));
                c.AddProducer<PaymentProcessFailIntegrationEvent>(nameof(PaymentProcessFailIntegrationEvent));


                c.AddConsumer<EventDispatcher>();
                c.UsingKafka((context, configurator) =>
                {
                    configurator.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));
                    configurator.TopicEndpoint<PaymentPrepareIntegrationEvent>(nameof(PaymentPrepareIntegrationEvent), "payemnt-prepare",
                        endpointConfigurator =>
                        {
                            endpointConfigurator.CreateIfMissing(n => n.NumPartitions = 1);
                            endpointConfigurator.AutoOffsetReset = AutoOffsetReset.Earliest;
                            endpointConfigurator.ConfigureConsumer<EventDispatcher>(context);
                        });
                });
            });
        });
        
        action?.Invoke(services);
        return services;
    }

    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        services.AddDbContextCustom<PaymentContext>(configuration, typeof(PaymentContext));
        services.AddHostedService<PaymentMigrationHostedService>();
        action?.Invoke(services);
        return services;
    }
}