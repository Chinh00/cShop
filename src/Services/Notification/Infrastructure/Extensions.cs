using Confluent.SchemaRegistry;
using cShop.Infrastructure.Cdc;
using Infrastructure.Cdc;
using IntegrationEvents;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCdcConsumer(this IServiceCollection services, IConfiguration configuration,
        Action<IServiceCollection>? action = null)
    {


         services.AddKafkaConsumer<OrderConsumerConfig>(e =>
         {
             e.Topic = "order_cdc_events";
             e.GroupId = "order_cdc_events_notification_group";
             e.HandlePayload = async (ISchemaRegistryClient schemaRegistryClient, string eventName, byte[] payload) =>
             {
                 return await (eventName switch
                 {
                     nameof(OrderConfirmIntegrationEvent) => payload.AsRecord<OrderConfirmIntegrationEvent>(schemaRegistryClient),
                     _ => throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null)
                 });
             };
         });
        
        action?.Invoke(services);
        return services;
    }
}