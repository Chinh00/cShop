using cShop.Core.Repository;
using Domain;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Cdc;

public class ProductCdcConsumer(IRepository<ProductInfo> productRepository, ILogger<ProductCdcConsumer> logger) : INotificationHandler<ProductCreated>
{
    
    
    public async Task Handle(ProductCreated notification, CancellationToken cancellationToken)
    {
        
        await productRepository.AddAsync(new ProductInfo()
        {
            Id = Guid.Parse(notification.Id),
            ProductName = notification.Name,
            ProductPrice = notification.Price
        }, cancellationToken);
        logger.LogInformation($"Product created {notification.Id}.");
    }
}