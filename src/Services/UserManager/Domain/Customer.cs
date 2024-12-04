using cShop.Core.Domain;

namespace Domain;

public class Customer : AggregateBase
{
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}