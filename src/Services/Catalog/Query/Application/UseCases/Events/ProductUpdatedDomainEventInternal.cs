
using cShop.Contracts.Events.DomainEvents;
using MediatR;
using Projections;

namespace Application.UseCases.Events;

public class ProductUpdatedDomainEventInternal : INotificationHandler<ProductNameUpdatedDomainEvent>
{
    private readonly IProjectionRepository<CatalogProjection> _catalogProjectionRepository;
    private readonly ILogger<ProductUpdatedDomainEventInternal> _logger;

    public ProductUpdatedDomainEventInternal(IProjectionRepository<CatalogProjection> catalogProjectionRepository, ILogger<ProductUpdatedDomainEventInternal> logger)
    {
        _catalogProjectionRepository = catalogProjectionRepository;
        _logger = logger;
    }

    public async Task Handle(ProductNameUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"ProductNameUpdatedDomainEvent internal: {notification}");
        await _catalogProjectionRepository.UpdateFieldAsync(notification.ProductId, notification.Version, e => e.Name,
            notification.Name, cancellationToken);
        await _catalogProjectionRepository.UpdateFieldAsync(notification.ProductId, notification.Version, e => e.Version,
            notification.Version, cancellationToken);
    }
}