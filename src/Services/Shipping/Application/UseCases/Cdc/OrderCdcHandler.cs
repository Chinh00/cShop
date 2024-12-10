using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Cdc;

public class OrderCdcHandler(
    ITopicProducer<ShipmentCreatedIntegrationEvent> shipmentCreatedProducer,
    IRepository<ShipperOrder> shipperOrderRepository)
    : INotificationHandler<OrderConfirmIntegrationEvent>
{


    public async Task Handle(OrderConfirmIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await shipperOrderRepository.AddAsync(new ShipperOrder()
        {
            Id = Guid.Parse(notification.OrderId)
        }, cancellationToken);
        await shipmentCreatedProducer.Produce(new { notification.OrderId }, cancellationToken);
    }
}