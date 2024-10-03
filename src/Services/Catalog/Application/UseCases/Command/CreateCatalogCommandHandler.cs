using cShop.Contracts.Abstractions;
using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public class CreateCatalogCommandHandler
    : IRequestHandler<Commands.CreateCatalog, ResultModel<Guid>>
{
    private readonly ILogger<CreateCatalogCommandHandler> _logger;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IBusEvent _messageBus;

    public CreateCatalogCommandHandler(ILogger<CreateCatalogCommandHandler> logger, IEventStoreRepository eventStoreRepository, IBusEvent messageBus)
    {
        _logger = logger;
        _eventStoreRepository = eventStoreRepository;
        _messageBus = messageBus;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.CreateCatalog request, CancellationToken cancellationToken)
    {
        Catalog catalog = new();
        catalog.CreateCatalog(request.Command(request.Name, request.Quantity, request.Price, request.ImageSrc, request.CategoryId));
        
        foreach (var catalogDomainEvent in catalog.DomainEvents)
        {
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(catalog, catalogDomainEvent),
                cancellationToken);
            await _messageBus.Publish((dynamic)catalogDomainEvent, cancellationToken);
        }
        await _messageBus.Publish(new IntegrationEvent.CatalogCreatedIntegration(catalog.Id, catalog.Name, catalog.Price), cancellationToken);
        
        return ResultModel<Guid>.Create(catalog.Id);
    }
}