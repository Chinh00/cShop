using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface OrderSubmitted : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
};