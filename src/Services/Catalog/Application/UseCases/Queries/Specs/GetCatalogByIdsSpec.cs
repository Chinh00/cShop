using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain.Aggregate;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogByIdsSpec : ListSpecification<CatalogItem>
{
    public GetCatalogByIdsSpec(List<Guid> catalogIds)
    {
        ApplyFilterList(catalogIds.Select(e => new FilterModel("Id", "==", e.ToString())).ToList());
    }
}