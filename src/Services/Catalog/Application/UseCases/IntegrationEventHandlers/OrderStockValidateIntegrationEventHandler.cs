using IntegrationEvents;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public class OrderStockValidateIntegrationEventHandler : INotificationHandler<MakeOrderStockValidateIntegrationEvent>
{
    public async Task Handle(MakeOrderStockValidateIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("OrderStockValidateIntegrationEventHandler");
    }
}