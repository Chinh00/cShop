using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface OrderStartedIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    
    public List<OrderDetail> OrderDetails { get; set; }
    
    public record OrderDetail(Guid CatalogId, int Quantity) : IIntegrationEvent;
};