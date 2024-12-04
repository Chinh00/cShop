using cShop.Core.Repository;
using Domain.Aggregate;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public class OrderStockValidateIntegrationEventHandler(
    ITopicProducer<OrderStockValidatedSuccessIntegrationEvent> orderStockValidateSuccess, 
    ITopicProducer<OrderStockValidatedFailIntegrationEvent> orderStockValidateFail,
    IRepository<CatalogItem> repository)
    : INotificationHandler<MakeOrderStockValidateIntegrationEvent>
{
    public async Task Handle(MakeOrderStockValidateIntegrationEvent notification, CancellationToken cancellationToken)
    {
        List<CatalogItem> catalogItems;

        await orderStockValidateSuccess.Produce(new { notification.OrderId }, cancellationToken);
    }
    
}
