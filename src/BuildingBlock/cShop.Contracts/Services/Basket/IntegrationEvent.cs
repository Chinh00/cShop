using cShop.Core.Domain;

namespace cShop.Contracts.Services.Basket;

public static class IntegrationEvent
{
    public record OrderCreated(Guid OrderId, Guid UserId) : Message, IIntegrationEvent;
} 