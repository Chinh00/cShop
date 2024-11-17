using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain.Aggregate;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Command;

public record ActiveCatalogCommand(Guid CatalogId) : ICommand<IResult>
{
    
    
    public class Validator : AbstractValidator<ActiveCatalogCommand>
    {
        public Validator()
        {
            RuleFor(x => x.CatalogId).NotEmpty();
        }
    }

    internal class Handler(IRepository<CatalogItem> productRepository) : IRequestHandler<ActiveCatalogCommand, IResult>
    {
        public async Task<IResult> Handle(ActiveCatalogCommand request, CancellationToken cancellationToken)
        {

            var product = await productRepository.FindByIdAsync(request.CatalogId);
            product.ActiveCatalog();
            await productRepository.UpdateAsync(product, cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(product.Id));
        }
    }
}