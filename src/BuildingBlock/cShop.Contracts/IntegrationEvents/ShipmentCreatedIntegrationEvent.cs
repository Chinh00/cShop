using cShop.Core.Domain;

namespace IntegrationEvents;

public record ShipmentCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; init; }
}