namespace Domain;

using cShop.Core.Domain;


public class ShipperInfo : AggregateBase
{
    
    public string Name { get; set; }
    
    public virtual ICollection<ShipperOrder> ShipperOrders { get; set; }
    
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}