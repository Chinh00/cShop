using cShop.Core.Domain;

namespace IntegrationEvents;

public class UserCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid UserId { get; set; }
}