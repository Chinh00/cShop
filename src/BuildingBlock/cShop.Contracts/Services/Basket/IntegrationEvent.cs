using cShop.Core.Domain;

namespace cShop.Contracts.Services.Basket;

public static class IntegrationEvent
{
    public record OrderCreated(Guid OrderId, Guid UserId) : Message, IIntegrationEvent;
    
    public record BasketCheckoutSuccess(Guid OrderId) : Message, IIntegrationEvent;
    public record BasketCheckoutFail(Guid OrderId) : Message, IIntegrationEvent;
    
    
} 