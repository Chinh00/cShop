using Application.UseCases.Dtos;
using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetShippersSpec : ListSpecification<Shipper>
{
    public GetShippersSpec(IQuery<ListResultModel<ShipperDto>> query)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        ApplyPagination(query.Page, query.PageSize);
    }
}