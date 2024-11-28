using cShop.Core.Domain;

namespace Domain;

public class Shipper : AggregateBase
{
    
    public string Name { get; set; }
    
    public virtual ICollection<ShipperOrder> ShipperOrders { get; set; }
    
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}