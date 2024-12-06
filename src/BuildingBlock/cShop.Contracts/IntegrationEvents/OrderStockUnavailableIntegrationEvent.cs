using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockUnavailableIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}