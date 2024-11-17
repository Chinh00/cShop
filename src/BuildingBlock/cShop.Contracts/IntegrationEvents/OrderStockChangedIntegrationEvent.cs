using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockChangedIntegrationEvent(Guid OrderId): IIntegrationEvent
{
    
}