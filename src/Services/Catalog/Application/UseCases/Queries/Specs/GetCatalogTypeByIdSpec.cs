using cShop.Core.Specifications;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogTypeByIdSpec : SpecificationBase<CatalogType>
{
    public GetCatalogTypeByIdSpec(Guid catalogTypeId)
    {
        ApplyFilter(e => e.Id == catalogTypeId);
    }
}