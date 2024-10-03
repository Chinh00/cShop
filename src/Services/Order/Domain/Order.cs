using cShop.Core.Domain;

namespace Domain;

public class Order : EntityBase
{
    public Guid CustomerId { get; set; }
    public virtual CustomerInfo Customer { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = [];
    
}