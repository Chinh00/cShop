using Application.UseCases.Dtos;
using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain.Entities;

namespace Application.UseCases.Queries.Specs;

public sealed class GetCatalogBrandsSpec : ListSpecification<CatalogBrand>
{
    public GetCatalogBrandsSpec(IQuery<ListResultModel<CatalogBrandDto>> query)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        ApplyPagination(query.Page, query.PageSize);
    }
}