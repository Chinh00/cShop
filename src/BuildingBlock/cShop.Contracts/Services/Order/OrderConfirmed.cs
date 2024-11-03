using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface OrderConfirmed : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid TransactionId { get; set; }
}