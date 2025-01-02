using Confluent.SchemaRegistry;
using cShop.Contracts.Services.Order;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain;
using Domain.Outbox;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Masstransits;

public class OrderConfirmConsumer(
    ISchemaRegistryClient schemaRegistryClient,
    IRepository<Order> orderRepository,
    IRepository<OrderOutbox> repository)
    : OutboxHandler<OrderOutbox>(schemaRegistryClient, repository), INotificationHandler<OrderConfirmed>
{
  
    public async Task Handle(OrderConfirmed notification, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindByIdAsync(notification.OrderId, default);
        var orderCreatedIntegrationEvent = new OrderConfirmIntegrationEvent()
        {
            OrderId = notification.OrderId.ToString()
        };
        await SendToOutboxAsync(order, () => (new OrderOutbox(), orderCreatedIntegrationEvent, "order_cdc_events"),
            default);
    }
}