using cShop.Core.Domain;

namespace IntegrationEvents;

public interface OrderStartedIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    
    public IList<OrderCheckoutDetail> OrderCheckoutDetails { get; set; }
    
};

public class OrderCheckoutDetail
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
