using cShop.Core.Domain;

namespace IntegrationEvents;

public record UserCreatedIntegrationEvent(Guid UserId, Guid Username) : IIntegrationEvent
{
    
}