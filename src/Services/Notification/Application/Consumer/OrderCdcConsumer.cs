using IntegrationEvents;
using MediatR;

namespace Application.Consumer;

public class OrderCdcConsumer(ILogger<OrderCdcConsumer> logger) : INotificationHandler<OrderConfirmIntegrationEvent>
{
    public async Task Handle(OrderConfirmIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Order {notification.OrderId} has been processed");
        
    }
}