using Identity.Api.Models;
using Identity.Api.Services;
using IntegrationEvents;
using MediatR;

namespace Identity.Api.Consumers;

public sealed class CustomerCdcConsumer(UserManager userManager) : INotificationHandler<CustomerCreatedIntegrationEvent>
{
    
    public async Task Handle(CustomerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await userManager.CreateAsync(new ApplicationUser()
        {
            Id = Guid.Parse(notification.Id).ToString(),
            Email = notification.Email,
            PhoneNumber = notification.PhoneNumber,
            UserName = notification.Email ?? notification.PhoneNumber
        }, notification.Password);
    }
}