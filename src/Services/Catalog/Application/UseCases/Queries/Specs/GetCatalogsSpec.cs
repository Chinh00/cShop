using cShop.Core.Specifications;
using Domain.Aggregate;

namespace Application.UseCases.Queries.Specs;

public class GetCatalogsSpec : ListSpecification<CatalogItem>
{
    public GetCatalogsSpec(GetCatalogsQuery query)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        ApplyPagination(query.Page, query.PageSize);
    }
}