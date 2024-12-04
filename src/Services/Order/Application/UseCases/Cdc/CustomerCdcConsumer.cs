using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class CustomerCdcConsumer(IRepository<CustomerInfo> customerRepository)
    : INotificationHandler<CustomerCreatedIntegrationEvent>
{

    public async Task Handle(CustomerCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await customerRepository.AddAsync(new CustomerInfo()
        {
            Id = Guid.Parse(notification.Id),
        }, cancellationToken);
    }
}