using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderPaidIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public List<OrderCheckoutDetail> OrderCheckoutDetails { get; set; }
    
};

