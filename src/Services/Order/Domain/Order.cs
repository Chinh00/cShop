using cShop.Core.Domain;
using Domain.Enums;

namespace Domain;

public class Order : EntityBase
{
    public Guid CustomerId { get; set; }
    public virtual CustomerInfo Customer { get; set; }
    public DateTime OrderDate { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public float Discount { get; set; }
    
    public string Description { get; set; }
    
    public List<OrderDetail> OrderDetails { get; init; } = [];
    
    public OrderStatus OrderStatus { get; set; }

    public void AddOrderDetail(OrderDetail orderDetail)
    {
        OrderDetails.Add(orderDetail);
        TotalPrice += orderDetail.Quantity * orderDetail.ProductInfo.ProductPrice;
    }
    
}