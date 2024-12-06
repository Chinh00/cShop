using IntegrationEvents;
using MediatR;

namespace Application.Consumer;

public class OrderCdcConsumer(ILogger<OrderCdcConsumer> logger) : INotificationHandler<OrderComplete>
{

    public async Task Handle(OrderComplete notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Order {notification.OrderId} has been processed");        
    }
}