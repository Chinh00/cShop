using cShop.Core.Domain;

namespace IntegrationEvents;

public record ShipmentDeliveryIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; init; }
}