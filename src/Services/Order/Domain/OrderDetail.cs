using cShop.Core.Domain;

namespace Domain;

public class OrderDetail : EntityBase
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public virtual ProductInfo ProductInfo { get; set; }
}