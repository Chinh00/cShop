using cShop.Core.Domain;

namespace Domain.Entities;

public class Basket : AggregateBase
{
    public Guid UserId { get; set; }
    
    public float TotalPrice { get; set; }
    
    public List<BasketItem> Items { get; set; } = [];   
    
    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}