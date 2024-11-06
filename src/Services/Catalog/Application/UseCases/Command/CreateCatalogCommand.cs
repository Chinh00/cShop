

using Confluent.SchemaRegistry;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain.Aggregate;
using Domain.Outbox;
using FluentValidation;
using IntegrationEvents;

namespace Application.UseCases.Command;

public record CreateCatalogCommand(
    string Name, 
    int Quantity, 
    double Price, 
    string ImageSrc, 
    Guid? CategoryId) : ICommand<IResult>
{
    private cShop.Contracts.Services.Catalog.Command.CreateCatalog Command(string name, int quantity, double price, string imageSrc, Guid? categoryId) =>
        new(name, quantity, price, imageSrc, categoryId);
    
    
    public class Validator : AbstractValidator<CreateCatalogCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.ImageSrc).NotNull().NotEmpty();
        }
    }
    
    internal class Hander(
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<Product> catalogRepository,
        IRepository<ProductOutbox> repository)
        : OutboxHandler<ProductOutbox>(schemaRegistryClient, repository), IRequestHandler<CreateCatalogCommand, IResult>
    {
        public async Task<IResult> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {
            Product product = new();
            product.CreateCatalog(request.Command(request.Name, request.Quantity, request.Price, request.ImageSrc, request.CategoryId));
            await catalogRepository.AddAsync(product, cancellationToken);
            await SendToOutboxAsync(
                product,
                () => (
                    new ProductOutbox(),
                    new ProductCreated()
                        {
                            Id = product.Id.ToString(),
                            Name = product.Name,
                            Price = (float)product.Price
                        }, 
                    "catalog_cdc_events"), cancellationToken);
            
            return Results.Ok(ResultModel<Guid>.Create(product.Id));
        }
    }
}