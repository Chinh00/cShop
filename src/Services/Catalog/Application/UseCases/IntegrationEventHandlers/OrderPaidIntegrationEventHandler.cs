using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public class OrderPaidIntegrationEventHandler(
    ITopicProducer<OrderStockUnavailableIntegrationEvent> orderStockUnavailableProducer,
    ITopicProducer<OrderStockChangedIntegrationEvent> orderStockChangedProducer)
    : INotificationHandler<OrderPaidIntegrationEvent>
{

    

   

    public async Task Handle(OrderPaidIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await orderStockChangedProducer.Produce(new { notification.OrderId }, cancellationToken);
    }
}