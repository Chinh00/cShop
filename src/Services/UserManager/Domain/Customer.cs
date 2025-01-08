using cShop.Core.Domain;

namespace Domain;

public class Customer : AggregateBase
{
    
    public string Name { get; init; }
    public string Email { get; init; }
    
    public string PhoneNumber { get; init; }

    public string Address { get; init; }
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}