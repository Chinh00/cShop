using Application.UseCases.Queries.Specs;
using cShop.Core.Repository;
using Domain.Aggregate;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public sealed class OrderStockValidateIntegrationEventHandler(
    IListRepository<CatalogItem> repository,
    ITopicProducer<OrderStockValidatedSuccessIntegrationEvent> stockValidateSuccess,
    ITopicProducer<OrderStockValidatedFailIntegrationEvent> stockValidateFail)
    : INotificationHandler<MakeOrderStockValidateIntegrationEvent>
{

    public async Task Handle(MakeOrderStockValidateIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var catalogsSpec = new GetCatalogByIdsSpec(notification.OrderItems.Select(c => c.ProductId).ToList());
        var catalogs = await repository.FindAsync(catalogsSpec, cancellationToken);
        decimal totalAmount = 0;
        foreach (var notificationOrderItem in notification.OrderItems)
        {
            var catalogItem = catalogs.FirstOrDefault(c => c.Id == notificationOrderItem.ProductId);
            if (catalogItem is null)
            {
                await stockValidateFail.Produce(new
                {
                    notification.OrderId
                }, cancellationToken);
                return;
            }
            
            if (!catalogItem.IsAvailable(notificationOrderItem.Quantity))
            {
                await stockValidateFail.Produce(new
                {
                    notification.OrderId
                }, cancellationToken);
                return;
            }
            totalAmount += notificationOrderItem.Quantity * catalogItem.Price; 
        }
        
        await stockValidateSuccess.Produce(new { notification.OrderId, TotalAmount = totalAmount }, cancellationToken);
    }
}