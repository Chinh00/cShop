using cShop.Contracts.Events.DomainEvents;
using MediatR;
using Projections;

namespace Application.UseCases.Events;

public class ProductCreateDomainEventInternal : INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly ILogger<ProductCreateDomainEventInternal> _logger;
    private readonly IProjectionRepository<CatalogProjection> _repository;

    public ProductCreateDomainEventInternal(ILogger<ProductCreateDomainEventInternal> logger, IProjectionRepository<CatalogProjection> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product created {catalogId}", notification.ProductId);
        await _repository.ReplaceOrInsertEventAsync(
            new CatalogProjection()
                { Id = notification.ProductId, Version = notification.Version, Name = notification.Name, CurrentCost = notification.CurrentCost},
            e => e.Id == notification.ProductId && notification.Version > e.Version, cancellationToken);
    }
}