using Core;
using cShop.Infrastructure.Cdc;
using Infrastructure.Catalog;
using Infrastructure.Cdc;
using Infrastructure.Internal;
using IntegrationEvents;
using Nest;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddElasticSearchService(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {
        var elasticSearchOptions = new ElasticOptions();
        configuration.GetSection(ElasticOptions.Name).Bind(elasticSearchOptions);

        services.AddSingleton<IElasticClient>(new ElasticClient(new ConnectionSettings(new Uri(elasticSearchOptions.ConnectionString)).DisablePing()
            .SniffOnStartup(false)
            .SniffOnConnectionFault(false)));
        services.AddScoped<IElasticManager, ElasticManager>();
        services.AddScoped<ICatalogIndexManager, CatalogIndexManager>();
        services.AddHostedService<CatalogIndexInitializerHostedService>();
        
        
        action?.Invoke(services);
        return services;
    }
    public static IServiceCollection AddCdcConsumer(this IServiceCollection services,
        Action<IServiceCollection>? action = null)
    {
        services.AddKafkaConsumer<CatalogConsumerConfig>(e =>
        {
            e.Topic = "catalog_cdc_events";
            e.GroupId = "catalog_cdc_events_search_group";
            e.HandlePayload = async (schemaRegistryClient, eventName, payload) =>
            {

                return eventName switch
                {
                    nameof(ProductCreatedIntegrationEvent) => await payload.AsRecord<ProductCreatedIntegrationEvent>(schemaRegistryClient),
                    _ => throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null)
                };



            };
        });
        
        action?.Invoke(services);
        return services;
    }
}