using cShop.Core.Domain;

namespace IntegrationEvents;

public record ShipmentPickedIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; init; }
}