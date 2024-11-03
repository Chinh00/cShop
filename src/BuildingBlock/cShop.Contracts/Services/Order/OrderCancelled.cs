using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface OrderCancelled : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}
