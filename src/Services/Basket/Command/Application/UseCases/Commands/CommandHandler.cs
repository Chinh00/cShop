using cShop.Contracts.Services.Basket;
using cShop.Infrastructure.EventStore;
using cShop.Infrastructure.IdentityServer;
using MediatR;

namespace Application.UseCases.Commands;

public class CommandHandler : IRequestHandler<Command.CreateBasket, Guid>
{

    private readonly IClaimContextAccessor _contextAccessor;
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly ILogger<CommandHandler> _logger;

    public CommandHandler(IClaimContextAccessor contextAccessor, IEventStoreRepository eventStoreRepository, ILogger<CommandHandler> logger)
    {
        _contextAccessor = contextAccessor;
        _eventStoreRepository = eventStoreRepository;
        _logger = logger;
    }


    public async Task<Guid> Handle(Command.CreateBasket request, CancellationToken cancellationToken)
    {
        
        return _contextAccessor.GetUserId();
    }
}