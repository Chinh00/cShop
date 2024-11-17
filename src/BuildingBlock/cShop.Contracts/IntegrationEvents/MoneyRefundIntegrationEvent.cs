using cShop.Core.Domain;

namespace IntegrationEvents;

public record MoneyRefundIntegrationEvent(Guid OrderId) : IIntegrationEvent
{
    
}