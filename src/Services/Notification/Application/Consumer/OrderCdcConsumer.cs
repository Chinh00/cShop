using IntegrationEvents;
using MediatR;

namespace Application.Consumer;

public class OrderCdcConsumer(ILogger<OrderCdcConsumer> logger) : INotificationHandler<OrderConfirmIntegrationEvent>
{
    public Task Handle(OrderConfirmIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}