using Application.UseCases.Dtos;
using cShop.Core.Domain;
using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetCustomersSpec : ListSpecification<Customer>
{
    public GetCustomersSpec(IQuery<ListResultModel<CustomerDto>> query)
    {
        ApplyFilterList(query.Filters);
        ApplyIncludeList(query.Includes);
        ApplyOrderList(query.Sorts);
        
    }
}