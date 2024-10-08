using cShop.Contracts.Services.Basket;
using cShop.Core.Domain;
using Domain.Entities;

namespace Domain.Aggregate;

public class Basket : AggregateBase
{
    
    public Guid UserId { get; set; }
    
    public ICollection<BasketItem> BasketItems { get; set; }
    
    public double TotalPrice { get; set; }

    public void CreateBasket(Command.CreateBasket command)
    {
        UserId = command.UserId;
        BasketItems ??= [];
        TotalPrice = 0;
        RaiseEvent(version => new DomainEvents.BasketCreated(Id, UserId, version));
    }

    public void AddBasketItem(Command.AddBasketItem command)
    {
        BasketItems ??= [];
        BasketItems.Add(new BasketItem()
        {
            ProductId = command.ProductId,
            Quantity = command.Quantity,
            Price = command.Price
        });
        RaiseEvent(version => new DomainEvents.BasketItemAdded(Id, UserId, command.ProductId, command.Quantity, command.Price, version));
    }






    public override void ApplyEvent(IDomainEvent @event) => With(@event as dynamic);

    void With(DomainEvents.BasketCreated @event)
    {
        Id = @event.BasketId;
        UserId = @event.UserId;

        Version = @event.Version;
    }

    void With(DomainEvents.BasketItemAdded @event)
    {
        BasketItems ??= [];
        BasketItems.Add(new BasketItem()
        {
            ProductId = @event.ProductId,
            Quantity = @event.Quantity,
            Price = @event.Price
        });
        
        Version = @event.Version;
    }
}