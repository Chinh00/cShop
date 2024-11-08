using System.Security.Cryptography.X509Certificates;
using Confluent.Kafka;
using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using GrpcServices;
using Infrastructure.Consumers;
using MassTransit;



namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCatalogGrpcClient(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {

        services.AddGrpcClient<Catalog.CatalogClient>(
            o =>
            {
                o.Address = new Uri(configuration.GetValue<string>("CatalogGrpc:Url"));
            }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            var certificate = new X509Certificate2(configuration.GetValue<string>("Cert:Path"), configuration.GetValue<string>("Cert:Password"));

            handler.ClientCertificates.Add(certificate);
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            return handler;
        }); 
        action?.Invoke(services);
        return services;
    }

  
    
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