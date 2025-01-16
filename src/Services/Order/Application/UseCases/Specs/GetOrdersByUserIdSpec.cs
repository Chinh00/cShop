using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetOrdersByUserIdSpec<T> : ListSpecification<Order>
{
    public GetOrdersByUserIdSpec(IQuery<ListResultModel<T>> query, Guid userId)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        ApplyPagination(query.Page, query.PageSize);
    }
}