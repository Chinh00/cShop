using cShop.Core.Domain;

namespace Domain.Entities;

public class BasketItem : EntityBase
{
    public Guid ProductId { get; set; }
    
    public double Price { get; set; }
    
    public int Quantity { get; set; }
}