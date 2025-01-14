using cShop.Core.Specifications;
using Domain.Aggregate;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogByIdSpec : SpecificationBase<CatalogItem>
{
    public GetCatalogByIdSpec(Guid id)
    {
        ApplyFilter(e => e.Id == id);
        ApplyInclude(e => e.CatalogBrand);
        ApplyInclude(e => e.CatalogType);
        ApplyInclude(e => e.Pictures);
    }
}