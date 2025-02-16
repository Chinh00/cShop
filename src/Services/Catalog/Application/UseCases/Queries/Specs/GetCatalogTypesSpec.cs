using Application.UseCases.Dtos;
using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogTypesSpec : ListSpecification<CatalogType>
{
    public GetCatalogTypesSpec(IQuery<ListResultModel<CatalogTypeDto>> query)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        ApplyPagination(query.Page, query.PageSize);
    }
}