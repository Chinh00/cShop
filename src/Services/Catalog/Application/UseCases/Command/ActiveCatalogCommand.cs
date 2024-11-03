using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public record ActiveCatalogCommand(Guid CatalogId) : ICommand<IResult>
{
    internal class Handler : IRequestHandler<ActiveCatalogCommand, IResult>
    {
        private readonly ILogger<ActiveCatalogCommand> _logger;
        private readonly IBusEvent _message;

        public Handler( ILogger<ActiveCatalogCommand> logger, IBusEvent message)
        {
            _logger = logger;
            _message = message;
        }

        public async Task<IResult> Handle(ActiveCatalogCommand request, CancellationToken cancellationToken)
        {
            
            return Results.Ok(ResultModel<Guid>.Create(Guid.NewGuid()));
        }
    }
}