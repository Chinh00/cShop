using cShop.Core.Domain;

namespace IntegrationEvents;

public interface BasketCheckoutFailIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}