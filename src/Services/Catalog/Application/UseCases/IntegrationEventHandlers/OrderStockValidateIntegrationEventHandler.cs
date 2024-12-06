using cShop.Core.Repository;
using Domain.Aggregate;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.IntegrationEventHandlers;

public sealed class OrderStockValidateIntegrationEventHandler(
    IRepository<CatalogItem> catalogItemRepository,
    ITopicProducer<OrderStockValidatedSuccessIntegrationEvent> stockValidateSuccess,
    ITopicProducer<OrderStockValidatedFailIntegrationEvent> stockValidateFail)
    : INotificationHandler<MakeOrderStockValidateIntegrationEvent>
{

    public async Task Handle(MakeOrderStockValidateIntegrationEvent notification, CancellationToken cancellationToken)
    {
        // foreach (var notificationOrderItem in notification.OrderItems)
        // {
        //     var catalogItem =
        //         await catalogItemRepository.FindByIdAsync(notificationOrderItem.ProductId, cancellationToken);
        //     if (catalogItem.AvailableStock < notificationOrderItem.Quantity)
        //     {
        //         await stockValidateFail.Produce(new { notification.OrderItems }, cancellationToken);
        //         return;
        //     }
        // }
        await stockValidateSuccess.Produce(new { notification.OrderId }, cancellationToken);
    }
}