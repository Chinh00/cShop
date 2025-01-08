using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public class GetProductInfoByListIdSpec : ListSpecification<ProductInfo>
{
    public GetProductInfoByListIdSpec(List<Guid> ids)
    {
        ids.ForEach(e => ApplyFilter(c => c.Id == e));
    }
}