using Infrastructure.Catalog;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class CatalogCdcConsumer(ICatalogIndexManager catalogIndexManager) : INotificationHandler<ProductCreatedIntegrationEvent>
{
    public async Task Handle(ProductCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Catalog catalog index started");
        var catalogIndexModel = new CatalogIndexModel()
        {
            Id = Guid.Parse(notification.Id),
            CatalogName = notification.Name,
            Price = notification.Price,
            Description = notification.Description,
            CatalogTypeId = Guid.Parse(notification.CatalogTypeId),
            CatalogBrandId = Guid.Parse(notification.CatalogBrandId),
            CatalogTypeName = notification.CatalogTypeName,
            CatalogBrandName = notification.CatalogBrandName
        };
        await catalogIndexManager.AddOrUpdateAsync(catalogIndexModel);
    }
}