

using Application.UseCases.Queries.Specs;
using Confluent.SchemaRegistry;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain.Aggregate;
using Domain.Entities;
using Domain.Outbox;
using FluentValidation;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Command;

public record CreateCatalogCommand(
    string Name, 
    int AvailableStock, 
    decimal Price, 
    string Description,
    List<string> Pictures, 
    CreateCatalogCommand.CatalogTypeCreateModel CatalogType,
    CreateCatalogCommand.CatalogBrandCreateModel CatalogBrand) : ICommand<IResult>
{
    public record CatalogTypeCreateModel(Guid? Id, string Name);
    public record CatalogBrandCreateModel(Guid? Id, string Name);
    public class Validator : AbstractValidator<CreateCatalogCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.AvailableStock).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.Pictures).NotNull().NotEmpty();
        }
    }
    
    internal class Hander(
        ISchemaRegistryClient schemaRegistryClient,
        IRepository<CatalogItem> catalogRepository,
        IRepository<CatalogOutbox> repository,
        IRepository<CatalogBrand> catalogBrandRepository,
        IRepository<CatalogType> catalogTypeRepository)
        : OutboxHandler<CatalogOutbox>(schemaRegistryClient, repository), IRequestHandler<CreateCatalogCommand, IResult>
    {
        public async Task<IResult> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
        {
            CatalogItem catalogItem = new();
            var (name, availableStock, price, description, pictures, catalogType, categoryBrandId) = request;
            var (catalogTypeId, catalogTypeName) = catalogType;
            var (catalogBrandId, catalogBrandName) = categoryBrandId;
            
            catalogItem.CreateCatalog(name, availableStock, price, description, pictures);



            CatalogType alreadyCatalogType;
            if (catalogTypeId is not null)
            {
                alreadyCatalogType = await catalogTypeRepository.FindOneAsync(new GetCatalogTypeByIdSpec(catalogTypeId.Value), cancellationToken);
            }
            else
            {
                alreadyCatalogType = await catalogTypeRepository.FindOneAsync(new GetCatalogTypeByNameSpec(catalogTypeName), cancellationToken);
            }

            catalogItem.AssignCatalogType(alreadyCatalogType ?? new CatalogType() {Name = catalogTypeName});

            CatalogBrand alreadyCatalogBrand;
            if (catalogBrandId is not null)
            {
                alreadyCatalogBrand = await catalogBrandRepository.FindOneAsync(new GetCatalogBrandByIdSpec(catalogBrandId.Value), cancellationToken);
            }
            else
            {
                alreadyCatalogBrand = await catalogBrandRepository.FindOneAsync(new GetCatalogBrandByNameSpec(catalogBrandName), cancellationToken);
            }

            catalogItem.AssignCatalogBrand(alreadyCatalogBrand ?? new CatalogBrand() {BrandName = catalogBrandName});
            
            

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