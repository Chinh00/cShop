using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public class DomainEvents
{
    public record OrderSubmitted(Guid OrderId, Guid UserId) : Message, IIntegrationEvent;

    public record MakeOrderValidate(Guid OrderId) : Message, IIntegrationEvent
    {
        
    }
    
    public record OrderCancelled(Guid OrderId, long Version) : Message, IDomainEvent;
    public record OrderConfirmed(Guid OrderId, long Version) : Message, IDomainEvent;
    
}