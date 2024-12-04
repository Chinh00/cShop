using cShop.Core.Domain;

namespace IntegrationEvents;

public interface OrderStartedIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; init; }
    public Guid UserId { get; init; }
    
    public List<OrderDetail> OrderDetails { get; init; }
    
    public record OrderDetail(Guid CatalogId, int Quantity) : IIntegrationEvent;
};