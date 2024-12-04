using cShop.Core.Domain;

namespace Domain;

public class ShipperOrder : EntityBase
{
    public DateTime ShipperOrderDate { get; private set; }
    public Guid OrderId { get; set; }
    public Guid? ShipperId { get; set; }
    public virtual ShipperInfo ShipperInfo { get; set; }
}