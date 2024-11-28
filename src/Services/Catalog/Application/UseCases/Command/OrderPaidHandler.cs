using cShop.Core.Repository;
using Domain.Aggregate;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Command;

public class OrderPaidHandler(IRepository<CatalogItem> repository, ITopicProducer<OrderStockChangedIntegrationEvent> orderStockChangedProducer, ITopicProducer<OrderStockUnavailableIntegrationEvent> orderStockUnavailableProducer) : INotificationHandler<OrderPaidIntegrationEvent>
{

    public async Task Handle(OrderPaidIntegrationEvent notification, CancellationToken cancellationToken)
    {
        foreach (var notificationCatalogItem in notification.CatalogItems)
        {
            var catalog = await repository.FindByIdAsync(notificationCatalogItem.ProductId, cancellationToken);
            catalog.RemoveStock(notificationCatalogItem.Quantity);
            await repository.UpdateAsync(catalog, cancellationToken);
        }
        await orderStockChangedProducer.Produce(new {notification.OrderId}, cancellationToken);        
        
    }
}