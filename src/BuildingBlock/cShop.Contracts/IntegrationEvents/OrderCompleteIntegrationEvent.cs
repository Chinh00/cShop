using cShop.Core.Domain;

namespace IntegrationEvents;

public class OrderCompleteIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}