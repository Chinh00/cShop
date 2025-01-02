namespace cShop.Contracts.Services.Order;

public class OrderStatus
{
    public Guid OrderId { get; set; }
    public string State { get; set; }
}