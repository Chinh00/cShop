using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public class GetProductInfoByListIdSpec : ListSpecification<ProductInfo>
{
    public GetProductInfoByListIdSpec(List<Guid> ids)
    {
        ApplyFilter(c => ids.Contains(c.Id));
    }
}