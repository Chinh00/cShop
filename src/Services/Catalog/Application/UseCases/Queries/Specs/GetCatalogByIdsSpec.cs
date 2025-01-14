using cShop.Core.Specifications;
using Domain.Aggregate;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogByIdsSpec : ListSpecification<CatalogItem>
{
    public GetCatalogByIdsSpec(List<Guid> catalogIds)
    {
        ApplyFilter(c => catalogIds.Contains(c.Id));
    }
}