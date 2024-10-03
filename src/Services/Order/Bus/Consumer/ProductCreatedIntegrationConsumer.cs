using cShop.Contracts.Services.Catalog;
using Domain;
using Infrastructure.Data;
using MassTransit;

namespace Bus.Consumer;

public class ProductCreatedIntegrationConsumer : IConsumer<IntegrationEvent.CatalogCreatedIntegration>
{
    private readonly OrderContext _context;
    private readonly ILogger<ProductCreatedIntegrationConsumer> _logger;

    public ProductCreatedIntegrationConsumer(OrderContext context, ILogger<ProductCreatedIntegrationConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IntegrationEvent.CatalogCreatedIntegration> context)
    {
        _logger.LogInformation("IntegrationEvent: ProductCreatedIntegrationConsumer");
        
        await _context.Set<ProductInfo>().AddAsync(new ProductInfo()
        {
            Id = context.Message.Id,
            ProductName = context.Message.ProductName,
            ProductPrice = context.Message.Price
        });
        await _context.SaveChangesAsync();
    }
}