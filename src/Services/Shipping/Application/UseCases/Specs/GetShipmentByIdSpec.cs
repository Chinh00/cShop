using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetShipmentByIdSpec : SpecificationBase<ShipperOrder>
{
    public GetShipmentByIdSpec(Guid orderId)
    {
        ApplyFilter(e => e.Id == orderId);
    }
}