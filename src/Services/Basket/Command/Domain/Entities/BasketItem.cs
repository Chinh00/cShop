using cShop.Core.Domain;

namespace Domain.Entities;

public class BasketItem : EntityBase
{
    
    public Guid BasketId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public float Price { get; set; }
}