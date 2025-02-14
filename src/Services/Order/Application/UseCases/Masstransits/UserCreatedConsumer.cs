using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Masstransits;

public class UserCreatedConsumer(IRepository<CustomerInfo> customerRepository)
    : INotificationHandler<UserCreatedIntegrationEvent>
{
    public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await customerRepository.AddAsync(new CustomerInfo() { Id = notification.UserId }, cancellationToken);
    }
}