using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderPaidIntegrationEvent(Guid OrderId, ICollection<OrderCatalogItem> CatalogItems) : IIntegrationEvent;

public record OrderCatalogItem(Guid ProductId, int Quantity);