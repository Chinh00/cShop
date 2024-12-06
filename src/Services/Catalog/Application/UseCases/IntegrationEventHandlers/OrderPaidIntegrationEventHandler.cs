using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public class OrderPaidIntegrationEventHandler(
    ITopicProducer<OrderStockUnavailableIntegrationEvent> orderStockUnavailableProducer,
    ITopicProducer<OrderStockChangedIntegrationEvent> orderStockChangedProducer,
    ILogger<OrderPaidIntegrationEventHandler> logger)
    : INotificationHandler<OrderPaidIntegrationEvent>
{

    
   

    public async Task Handle(OrderPaidIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Paid Integration Event");        
        await orderStockChangedProducer.Produce(new { notification.OrderId }, cancellationToken);
    }
}