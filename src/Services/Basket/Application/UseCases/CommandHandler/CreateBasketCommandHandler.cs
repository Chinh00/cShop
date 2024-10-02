using Application.UseCases.Command;
using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using cShop.Infrastructure.IdentityServer;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.CommandHandler;

public class CreateBasketCommandHandler : IRequestHandler<Commands.CreateBasket, ResultModel<Guid>>
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IClaimContextAccessor _claimContextAccessor;
    private readonly ILogger<CreateBasketCommandHandler> _logger;
    private readonly IBusEvent _messageBus;
    
    public CreateBasketCommandHandler(IEventStoreRepository eventStoreRepository, IClaimContextAccessor claimContextAccessor, ILogger<CreateBasketCommandHandler> logger, IBusEvent messageBus)
    {
        _eventStoreRepository = eventStoreRepository;
        _claimContextAccessor = claimContextAccessor;
        _logger = logger;
        _messageBus = messageBus;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.CreateBasket request, CancellationToken cancellationToken)
    {
        Basket basket = new();
        basket.CreateBasket(request.Command(_claimContextAccessor.GetUserId()));
        
        foreach (var basketDomainEvent in basket.DomainEvents)
        {
            await _eventStoreRepository.AppendEventAsync(StoreEvent.Create(basket, basketDomainEvent), cancellationToken);
            await _messageBus.Publish((dynamic)basketDomainEvent, cancellationToken);
        }
        
        
        
        
        return ResultModel<Guid>.Create(basket.Id);
    }
}