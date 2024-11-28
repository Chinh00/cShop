using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Aggregate;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Command;

public record InactiveCatalogCommand(Guid CatalogId) : ICommand<IResult>
{
    
    public class Validator : AbstractValidator<InactiveCatalogCommand>
    {
        public Validator()
        {
            RuleFor(x => x.CatalogId).NotEmpty();
        }
    }
    
    internal class Handler(IRepository<CatalogItem> productRepository)
        : IRequestHandler<InactiveCatalogCommand, IResult>
    {

        public async Task<IResult> Handle(InactiveCatalogCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindByIdAsync(request.CatalogId, cancellationToken);
            product.InActiveCatalog();
            await productRepository.UpdateAsync(product, cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(product.Id));
        }
    }
}