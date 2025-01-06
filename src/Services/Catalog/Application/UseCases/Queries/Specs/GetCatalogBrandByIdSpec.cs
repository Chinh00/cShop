using cShop.Core.Specifications;
using Domain.Aggregate;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogBrandByIdSpec : SpecificationBase<CatalogBrand>
{
    public GetCatalogBrandByIdSpec(Guid id)
    {
        ApplyFilter(e => e.Id == id);
    }
}