using cShop.Core.Domain;
using cShop.Contracts.Services.Basket;
namespace Domain.Entities;

public class Basket : AggregateBase
{
    public Guid UserId { get; set; }
    
    public float TotalPrice { get; set; }
    
    public List<BasketItem> BasketItems { get; set; }


    public void CreateBasket(Command.CreateBasket createBasket)
    {
        UserId = createBasket.UserId;
        TotalPrice = 0;
        RaiseEvent(version => new DomainEvents.BasketCreated(Id, UserId, version ));
    }

    public void AddBasketItem(Command.AddBasketItem addBasketItem)
    {
        BasketItems ??= [];
        BasketItems.Add(new BasketItem()
        {
            ProductId = addBasketItem.ProductId,
            Quantity = addBasketItem.Quantity,
            Price = addBasketItem.Price
        });
        
        RaiseEvent(version => new DomainEvents.BasketItemAdded(Id, UserId, addBasketItem.ProductId, addBasketItem.Price, version));
    }


    public override void ApplyEvent(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}