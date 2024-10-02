using cShop.Contracts.Services.Catalog;
using cShop.Infrastructure.Projection;
using MassTransit;
using Projection;

namespace Bus.Consumer;

public class CatalogActiveDomainEventConsumer : IConsumer<DomainEvents.CatalogActivated>
{
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;
    private readonly ILogger<CatalogActiveDomainEventConsumer> _logger;

    public CatalogActiveDomainEventConsumer(IProjectionRepository<CatalogProjection> catalogProjectionRepository, ILogger<CatalogActiveDomainEventConsumer> logger)
    {
        _catalogProjectionRepository = catalogProjectionRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogActivated> context)
    {
        _logger.LogInformation("Catalog active domain event: {@context}", context.Message);
        await _catalogProjectionRepository.UpdateFieldAsync(context.Message.Id, context.Message.Version,
            e => e.IsActive, true, default);
        
        await _catalogProjectionRepository.UpdateFieldAsync(context.Message.Id, context.Message.Version,
            e => e.Version, context.Message.Version, default);
        
        
    }
}