using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public record InactiveCatalogCommand(Guid CatalogId) : ICommand<IResult>
{
    internal class Handler : IRequestHandler<InactiveCatalogCommand, IResult>
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly ILogger<InactiveCatalogCommand> _logger;
        private readonly IBusEvent _message;

        public Handler(IEventStoreRepository eventStoreRepository, ILogger<InactiveCatalogCommand> logger, IBusEvent message)
        {
            _eventStoreRepository = eventStoreRepository;
            _logger = logger;
            _message = message;
        }

        public async Task<IResult> Handle(InactiveCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await _eventStoreRepository.LoadAggregateEventsAsync<Catalog>(request.CatalogId, cancellationToken);
            catalog.InActiveCatalog();
        
            foreach (var catalogDomainEvent in catalog.DomainEvents)
            {
                await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(catalog, catalogDomainEvent),
                    cancellationToken);
                await _message.Publish((dynamic) catalogDomainEvent, cancellationToken);
            }
            return Results.Ok(ResultModel<Guid>.Create(catalog.Id));
        }
    }
}