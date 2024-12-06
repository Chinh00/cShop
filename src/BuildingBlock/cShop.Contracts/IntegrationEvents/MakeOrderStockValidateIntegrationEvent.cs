using cShop.Core.Domain;
using MediatR;

namespace IntegrationEvents;

public record MakeOrderStockValidateIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    
    public List<OrderCheckoutDetail> OrderItems { get; set; }
    
}
  