using IntegrationEvents;
using MediatR;

namespace Application.Consumer;

public class OrderCdcConsumer : INotificationHandler<OrderComplete>
{
    public async Task Handle(OrderComplete notification, CancellationToken cancellationToken)
    {
        Console.Out.WriteLine(notification.OrderId);
        
    }
}