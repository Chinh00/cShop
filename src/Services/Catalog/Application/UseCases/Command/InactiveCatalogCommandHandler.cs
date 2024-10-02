using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public class InactiveCatalogCommandHandler : IRequestHandler<Commands.InActiveCatalog, ResultModel<Guid>>
{
    
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<ActiveCatalogCommandHandler> _logger;
    private readonly IBusEvent _message;

    public InactiveCatalogCommandHandler(IEventStoreRepository eventStoreRepository, ILogger<ActiveCatalogCommandHandler> logger, IBusEvent message)
    {
        _eventStoreRepository = eventStoreRepository;
        _logger = logger;
        _message = message;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.InActiveCatalog request, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreRepository.LoadAggregateEventsAsync<Catalog>(request.Id, cancellationToken);
        catalog.InActiveCatalog();
        
        foreach (var catalogDomainEvent in catalog.DomainEvents)
        {
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(catalog, catalogDomainEvent),
                cancellationToken);
            await _message.Publish((dynamic) catalogDomainEvent, cancellationToken);
        }
        return ResultModel<Guid>.Create(catalog.Id);
        
    }
}