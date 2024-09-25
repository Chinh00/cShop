using cShop.Contracts.Abstractions;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Entities;
using EventStore;
using MediatR;

namespace Application.UseCases.Commands;

public class CommandHandlers : IRequestHandler<Commands.CreateCatalog, Guid>
{
    private readonly ILogger<CommandHandlers> _logger;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IBusEvent _busEvent;

    public CommandHandlers(ILogger<CommandHandlers> logger, IEventStoreRepository eventStoreRepository, IBusEvent busEvent)
    {
        _logger = logger;
        _eventStoreRepository = eventStoreRepository;
        _busEvent = busEvent;
    }

    public async Task<Guid> Handle(Commands.CreateCatalog request, CancellationToken cancellationToken)
    {
        Product product = new();
        product.CreateProduct(request.Command(request.Name, request.CurrentCost, request.ImageSrc, request.CategoryId));
        
        foreach (var productDomainEvent in product.DomainEvents)
        {
            await _busEvent.Publish((dynamic)productDomainEvent, cancellationToken);
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(product, productDomainEvent),
                cancellationToken);
        }
        
        return Guid.NewGuid();
    }
}