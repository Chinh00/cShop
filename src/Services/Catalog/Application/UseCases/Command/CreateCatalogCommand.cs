

using cShop.Contracts.Abstractions;
using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;
namespace Application.UseCases.Command;

public record CreateCatalogCommand(string Name, int Quantity, double Price, string ImageSrc, Guid CategoryId) : ICommand<IResult>
{
    private cShop.Contracts.Services.Catalog.Command.CreateCatalog Command(string name, int quantity, double price, string imageSrc, Guid categoryId) =>
        new(name, quantity, price, imageSrc, categoryId);
    
    
    internal class Hander : IRequestHandler<CreateCatalogCommand, IResult>
    {
        private readonly ILogger<CreateCatalogCommand> _logger;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IBusEvent _messageBus;

        public Hander(ILogger<CreateCatalogCommand> logger, IEventStoreRepository eventStoreRepository, IBusEvent messageBus)
        {
            _logger = logger;
            _eventStoreRepository = eventStoreRepository;
            _messageBus = messageBus;
        }

        public async Task<IResult> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
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
        
            return Results.Ok(ResultModel<Guid>.Create(catalog.Id));
        }
    }
}