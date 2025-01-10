using cShop.Core.Domain;

namespace IntegrationEvents;

public class PaymentPrepareIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
}