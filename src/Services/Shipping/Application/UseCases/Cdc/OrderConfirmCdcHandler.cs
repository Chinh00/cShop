using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class OrderConfirmCdcHandler : INotificationHandler<OrderConfirmIntegrationEvent>
{
    public async Task Handle(OrderConfirmIntegrationEvent notification, CancellationToken cancellationToken)
    {
        
    }
}