using Confluent.SchemaRegistry;
using cShop.Contracts.Services.Order;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain;
using Domain.Outbox;
using IntegrationEvents;
using MassTransit;

namespace Application.UseCases.Masstransits;

public class OrderConfirmConsumer(
    ISchemaRegistryClient schemaRegistryClient,
    IRepository<Order> orderRepository,
    IRepository<OrderOutbox> repository)
    : OutboxHandler<OrderOutbox>(schemaRegistryClient, repository), IConsumer<OrderConfirmed>
{
    public async Task Consume(ConsumeContext<OrderConfirmed> context)
    {
        var order = await orderRepository.FindByIdAsync(context.Message.OrderId, default);
        var orderCreatedIntegrationEvent = new OrderConfirmIntegrationEvent()
        {
            OrderId = context.Message.OrderId.ToString()
        };
        await SendToOutboxAsync(order, () => (new OrderOutbox(), orderCreatedIntegrationEvent, "order_cdc_events"),
            default);
    }
    
    
}