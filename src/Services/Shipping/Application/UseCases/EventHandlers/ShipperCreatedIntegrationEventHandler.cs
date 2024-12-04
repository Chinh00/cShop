using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.EventHandlers;

public class ShipperCreatedIntegrationEventHandler(ILogger<ShipperCreatedIntegrationEventHandler> logger, IRepository<ShipperInfo> repository)
    : INotificationHandler<ShipperCreatedIntegrationEvent>
{
    public async Task Handle(ShipperCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Shipper Created Integration Event: {notification}");
        await repository.AddAsync(new ShipperInfo()
        {
            Id = Guid.Parse(notification.Id)
        }, cancellationToken);
    }
}