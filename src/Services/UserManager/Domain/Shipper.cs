using cShop.Core.Domain;

namespace Domain;

public class Shipper : AggregateBase
{
    public string Name { get; set; }
    public string Phone { get; set; }
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}