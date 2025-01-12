using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetOrderInfoByIdSpec : SpecificationBase<OrderInfo>
{
    public GetOrderInfoByIdSpec(Guid orderId, Guid userId)
    {
        ApplyFilter(e => e.OrderId == orderId && e.UserId == userId);
    }
}