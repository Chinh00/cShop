using cShop.Core.Domain;

namespace IntegrationEvents;

public interface BasketCheckoutSuccessIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}