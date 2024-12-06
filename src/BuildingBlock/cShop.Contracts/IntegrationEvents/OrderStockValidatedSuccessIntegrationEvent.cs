using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockValidatedSuccessIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}
