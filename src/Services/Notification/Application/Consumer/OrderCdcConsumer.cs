using IntegrationEvents;
using MediatR;

namespace Application.Consumer;

public class OrderCdcConsumer : INotificationHandler<OrderComplete>
{
    public async Task Handle(OrderComplete notification, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync(notification.OrderId);
        
    }
}