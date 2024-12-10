using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class ShipperCdcHandler(ILogger<ShipperCdcHandler> logger, IRepository<ShipperInfo> repository)
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