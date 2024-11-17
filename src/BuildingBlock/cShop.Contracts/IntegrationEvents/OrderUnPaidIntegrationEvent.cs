using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderUnPaidIntegrationEvent(Guid OrderId) : IIntegrationEvent
{
    
}