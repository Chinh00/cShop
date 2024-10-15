using cShop.Core.Domain;

namespace Domain.Entities;

public class Basket : EntityBase
{
    
    public Guid UserId { get; set; }

    public ICollection<BasketItem> BasketItems { get; set; }
    
    public double TotalPrice { get; set; }

    public void AddBasketItem(BasketItem item)
    {
        BasketItems ??= [];
        var basketItem = BasketItems.FirstOrDefault(e => e.BasketId == item.BasketId && e.ProductId == item.ProductId);

        if (basketItem is null)
        {
            BasketItems.Add(item);
        }
        else
        {
            basketItem.RecalculateQuantity(1);
        }
    }

    public void RemoveBasketItem(BasketItem item)
    {
        BasketItems ??= []; 
        BasketItems.Remove(item);
    }
}