

using Confluent.SchemaRegistry;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain.Aggregate;
using Domain.Outbox;
using FluentValidation;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Command;

public record CreateCatalogCommand(
    string Name, 
    int AvailableStock, 
    decimal Price, 
    string ImageSrc, 
    Guid? CategoryId) : ICommand<IResult>
{
    private cShop.Contracts.Services.Catalog.Command.CreateCatalog Command(string name, int quantity, decimal price, string imageSrc, Guid? categoryId) =>
        new(name, quantity, price, imageSrc, categoryId);
    
    
    public class Validator : AbstractValidator<CreateCatalogCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.AvailableStock).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.ImageSrc).NotNull().NotEmpty();
        }
    }
    
    internal class Hander(
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<CatalogItem> catalogRepository,
        IRepository<CatalogOutbox> repository)
        : OutboxHandler<CatalogOutbox>(schemaRegistryClient, repository), IRequestHandler<CreateCatalogCommand, IResult>
    {
        public async Task<IResult> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {
            CatalogItem catalogItem = new();
            catalogItem.CreateCatalog(request.Command(request.Name, request.AvailableStock, request.Price, request.ImageSrc, request.CategoryId));
            await catalogRepository.AddAsync(catalogItem, cancellationToken);
            await SendToOutboxAsync(
                catalogItem,
                () => (
                    new CatalogOutbox(),
                    new ProductCreated()
                        {
                            Id = catalogItem.Id.ToString(),
                            Name = catalogItem.Name,
                            Price = (float)catalogItem.Price
                        }, 
                    "catalog_cdc_events"), cancellationToken);
            
            return Results.Ok(ResultModel<Guid>.Create(catalogItem.Id));
        }
    }
}