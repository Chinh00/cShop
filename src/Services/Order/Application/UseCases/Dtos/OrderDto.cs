using cShop.Contracts.Services.Order;

namespace Application.UseCases.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public float Discount { get; set; }
    
    public string Description { get; set; }
    
    public List<OrderDetailDto> OrderDetails { get; init; } = [];
    
    public int OrderStatus { get; set; }
}