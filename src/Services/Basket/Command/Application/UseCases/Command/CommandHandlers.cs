using cShop.Infrastructure.IdentityServer;
using MediatR;

namespace Application.UseCases.Command;

public class CommandHandlers : IRequestHandler<Commands.CreateBasket, Guid>
{
    private readonly ILogger<CommandHandlers> _logger;
    private readonly IClaimContextAccessor _contextAccessor;

    public CommandHandlers(ILogger<CommandHandlers> logger, IClaimContextAccessor contextAccessor)
    {
        _logger = logger;
        _contextAccessor = contextAccessor;
    }

    public Task<Guid> Handle(Commands.CreateBasket request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}