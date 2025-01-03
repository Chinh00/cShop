using cShop.Core.Specifications;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogBrandByNameSpec : SpecificationBase<CatalogBrand>
{
    public GetCatalogBrandByNameSpec(string name)
    {
        ApplyFilter(e => e.BrandName.Contains(name));
    }
}