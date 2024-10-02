using Application.UseCases.Command;
using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using cShop.Infrastructure.IdentityServer;
using Domain.Aggregate;
using GrpcServices;
using MediatR;

namespace Application.UseCases.CommandHandler;

public class AddBasketItemCommandHandler : IRequestHandler<Commands.AddBasketItem, ResultModel<Guid>>
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<AddBasketItemCommandHandler> _logger;
    private readonly IBusEvent _messageBus;
    private readonly Catalog.CatalogClient _catalogClient;
    private readonly IClaimContextAccessor claimContextAccessor;

    public AddBasketItemCommandHandler(IEventStoreRepository eventStoreRepository, ILogger<AddBasketItemCommandHandler> logger, IBusEvent messageBus, Catalog.CatalogClient catalogClient, IClaimContextAccessor claimContextAccessor)
    {
        _eventStoreRepository = eventStoreRepository;
        _logger = logger;
        _messageBus = messageBus;
        _catalogClient = catalogClient;
        this.claimContextAccessor = claimContextAccessor;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.AddBasketItem request, CancellationToken cancellationToken)
    {
        var basket = await _eventStoreRepository.LoadAggregateEventsAsync<Basket>(request.BasketId, cancellationToken);
        var catalog = await _catalogClient.getProductByIdAsync(new GetCatalogByIdRequest()
        {
            Id = request.ProductId.ToString()
        });
        if (catalog is null) throw new Exception("Product not found");
        
        basket.AddBasketItem(request.Command(basket.Id, claimContextAccessor.GetUserId(), Guid.Parse(catalog.ProductId), request.Quantity, catalog.CurrentCost));
        
        foreach (var basketDomainEvent in basket.DomainEvents)
        {
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(basket, basketDomainEvent),
                cancellationToken);
            await _messageBus.Publish((dynamic) basketDomainEvent, cancellationToken);
        }

        return ResultModel<Guid>.Create(basket.Id);
    }
}