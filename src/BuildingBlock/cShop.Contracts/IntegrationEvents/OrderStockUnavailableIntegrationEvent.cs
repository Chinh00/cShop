using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockUnavailableIntegrationEvent(Guid OrderId) : IIntegrationEvent
{
    
}