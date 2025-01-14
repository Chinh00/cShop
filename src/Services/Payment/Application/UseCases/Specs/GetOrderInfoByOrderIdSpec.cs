using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public class GetOrderInfoByOrderIdSpec : SpecificationBase<OrderInfo>
{
    public GetOrderInfoByOrderIdSpec(Guid orderId, Guid userId)
    {
        ApplyFilter(e => e.OrderId == orderId && e.UserId == userId);
    }
}