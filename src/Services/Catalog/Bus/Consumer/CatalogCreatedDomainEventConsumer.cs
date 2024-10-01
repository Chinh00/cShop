using cShop.Contracts.Services.Catalog;
using cShop.Infrastructure.Projection;
using MassTransit;
using Projection;

namespace Bus.Consumer;

public class CatalogCreatedDomainEventConsumer : IConsumer<DomainEvents.CatalogCreated>
{
    private readonly ILogger<CatalogCreatedDomainEventConsumer> _logger;
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;
    public CatalogCreatedDomainEventConsumer(ILogger<CatalogCreatedDomainEventConsumer> logger, IProjectionRepository<CatalogProjection> catalogProjectionRepository)
    {
        _logger = logger;
        _catalogProjectionRepository = catalogProjectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.CatalogCreated> context)
    {
        _logger.LogInformation("Receive CatalogCreatedDomainEventConsumer");
        await _catalogProjectionRepository.ReplaceOrInsertEventAsync(new CatalogProjection()
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            Quantity = context.Message.Quantity,
            Price = context.Message.Price,
            ImageSrc = context.Message.ImageUrl,
            Version = context.Message.Version,
        }, e => e.Id == context.Message.Id && e.Version < context.Message.Version, default);

    }
}