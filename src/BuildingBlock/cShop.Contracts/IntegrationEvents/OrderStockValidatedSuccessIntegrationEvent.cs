using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockValidatedSuccessIntegrationEvent(Guid OrderId) : Message, IIntegrationEvent;
