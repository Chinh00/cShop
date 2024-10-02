using cShop.Contracts.Services.Catalog;
using cShop.Infrastructure.Projection;
using MassTransit;
using Projection;

namespace Bus.Consumer;

public class CatalogInactiveDomainEventConsumer : IConsumer<DomainEvents.CatalogInactivated>  
{
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;
    private readonly ILogger<CatalogInactiveDomainEventConsumer> _logger;

    public CatalogInactiveDomainEventConsumer(IProjectionRepository<CatalogProjection> catalogProjectionRepository, ILogger<CatalogInactiveDomainEventConsumer> logger)
    {
        _catalogProjectionRepository = catalogProjectionRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogInactivated> context)
    {
        await _catalogProjectionRepository.UpdateFieldAsync(context.Message.Id, context.Message.Version,
            e => e.IsActive, false, default);
        
        await _catalogProjectionRepository.UpdateFieldAsync(context.Message.Id, context.Message.Version,
            e => e.Version, context.Message.Version, default);
        
        
    }
}