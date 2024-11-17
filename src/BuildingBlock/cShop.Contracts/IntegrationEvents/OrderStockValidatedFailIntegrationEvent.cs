using cShop.Core.Domain;

namespace IntegrationEvents;
public record OrderStockValidatedFailIntegrationEvent(Guid OrderId) : Message, IIntegrationEvent;

