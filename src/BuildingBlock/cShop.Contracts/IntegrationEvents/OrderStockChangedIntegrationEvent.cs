using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockChangedIntegrationEvent: IIntegrationEvent
{
    public Guid OrderId { get; set; }
}