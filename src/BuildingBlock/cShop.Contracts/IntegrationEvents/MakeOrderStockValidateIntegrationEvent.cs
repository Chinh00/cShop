using cShop.Core.Domain;
using MediatR;

namespace IntegrationEvents;

public class MakeOrderStockValidateIntegrationEvent : INotification
{
    public Guid OrderId { get; set; }
    
    public List<OrderCheckoutDetail> OrderItems { get; set; }
    
}



