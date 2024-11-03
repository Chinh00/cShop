using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using cShop.Infrastructure.Bus;
using cShop.Infrastructure.EventStore;
using Domain.Aggregate;
using MediatR;

namespace Application.UseCases.Command;

public record InactiveCatalogCommand(Guid CatalogId) : ICommand<IResult>
{
    internal class Handler(ILogger<InactiveCatalogCommand> logger, IBusEvent message)
        : IRequestHandler<InactiveCatalogCommand, IResult>
    {
        private readonly ILogger<InactiveCatalogCommand> _logger = logger;

        public async Task<IResult> Handle(InactiveCatalogCommand request, CancellationToken cancellationToken)
        {
            
            
            return Results.Ok(ResultModel<Guid>.Create(Guid.NewGuid()));
        }
    }
}