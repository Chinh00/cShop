using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class CatalogCdcConsumer(IRepository<ProductInfo> productRepository, ILogger<CatalogCdcConsumer> logger) : INotificationHandler<ProductCreated>
{
    
    
    public async Task Handle(ProductCreated notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handle ProductCreated {notification.Id}");
        await productRepository.AddAsync(new ProductInfo()
        {
            Id = Guid.Parse(notification.Id),
            ProductName = notification.Name,
            ProductPrice = notification.Price
        }, cancellationToken);
    }
}