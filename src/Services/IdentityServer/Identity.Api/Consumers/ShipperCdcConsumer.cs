using MediatR;

namespace Identity.Api.Consumers;

public sealed class ShipperCdcConsumer(UserManager userManager) : INotificationHandler<ShipperCreatedIntegrationEvent>
{
    public async Task Handle(ShipperCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await userManager.CreateAsync(new ApplicationUser()
        {
            Id = Guid.Parse(notification.Id).ToString(),
            UserName = notification.Name,
            PhoneNumber = notification.Phone,
            Email = notification.Email,
        }, notification.Email);
    }
}