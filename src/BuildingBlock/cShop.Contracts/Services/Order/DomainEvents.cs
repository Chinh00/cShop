using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public class DomainEvents
{
    public record OrderSubmitted(Guid OrderId, long Version) : Message, IDomainEvent;

    public record MakeOrderValidate(Guid OrderId) : Message, IIntegrationEvent
    {
        
    }
}