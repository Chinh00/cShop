using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Masstransits;

public sealed class PaymentPrepareConsumer(IRepository<OrderInfo> orderRepository)
    : INotificationHandler<PaymentPrepareIntegrationEvent>
{

    public async Task Handle(PaymentPrepareIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await orderRepository.AddAsync(new OrderInfo()
        {
            OrderId = notification.OrderId,
            UserId = notification.UserId,
            Amount = notification.Amount,
            Status = PaymentStatus.Pending
        }, cancellationToken);
    }
}