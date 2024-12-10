using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockValidatedFailIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
};
