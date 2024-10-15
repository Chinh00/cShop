using Bus.Consumers;
using Confluent.Kafka;
using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using MassTransit;

namespace Bus;

public static class Extensions
{
    public static IServiceCollection AddCustomMasstransit(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddMassTransit(e =>
        {
            e.SetKebabCaseEndpointNameFormatter();

            e.UsingInMemory();
            
            e.AddRider(t =>
            {
                t.AddProducer<IntegrationEvent.BasketCheckoutSuccess>(nameof(IntegrationEvent.BasketCheckoutSuccess));
                t.AddProducer<IntegrationEvent.BasketCheckoutFail>(nameof(IntegrationEvent.BasketCheckoutFail));

                t.AddConsumer<MakeOrderValidateConsumer>();
                
                t.UsingKafka((context, k) =>
                {
                    k.Host(configuration.GetValue<string>("Kafka:BootstrapServers"));


                    k.TopicEndpoint<MakeOrderValidate>(nameof(MakeOrderValidate), "basket-group", c =>
                    {
                        c.CreateIfMissing(n => n.NumPartitions = 1);
                        c.AutoOffsetReset = AutoOffsetReset.Earliest;
                        c.ConfigureConsumer<MakeOrderValidateConsumer>(context);
                    });
                });
            });

        });
        
        action?.Invoke(services);
        return services;
    }
}