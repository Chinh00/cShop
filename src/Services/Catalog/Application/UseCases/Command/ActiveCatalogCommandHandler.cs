using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public class ActiveCatalogCommandHandler : IRequestHandler<Commands.ActiveCatalog, ResultModel<Guid>>
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<ActiveCatalogCommandHandler> _logger;
    private readonly IBusEvent _message;

    public ActiveCatalogCommandHandler(IEventStoreRepository eventStoreRepository, ILogger<ActiveCatalogCommandHandler> logger, IBusEvent message)
    {
        _eventStoreRepository = eventStoreRepository;
        _logger = logger;
        _message = message;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.ActiveCatalog request, CancellationToken cancellationToken)
    {
        var catalog = await _eventStoreRepository.LoadAggregateEventsAsync<Catalog>(request.Id, cancellationToken);
        catalog.ActiveCatalog();
        
        foreach (var catalogDomainEvent in catalog.DomainEvents)
        {
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(catalog, catalogDomainEvent), cancellationToken);
            await _message.Publish((dynamic) catalogDomainEvent, cancellationToken);
        }
        return ResultModel<Guid>.Create(catalog.Id);
    }
}