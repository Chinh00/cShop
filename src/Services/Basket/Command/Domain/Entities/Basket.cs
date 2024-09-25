using cShop.Core.Domain;
using cShop.Contracts.Services.Basket;
namespace Domain.Entities;

public class Basket : AggregateBase
{
    public Guid UserId { get; set; }
    
    public float TotalPrice { get; set; }
    
    public List<BasketItem> BasketItems { get; set; } = [];


    public void CreateBasket(Command.CreateBasket createBasket)
    {
        UserId = createBasket.UserId;
        RaiseEvent(version => new DomainEvents.BasketCreated(Id, UserId, version ));
    }



    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}