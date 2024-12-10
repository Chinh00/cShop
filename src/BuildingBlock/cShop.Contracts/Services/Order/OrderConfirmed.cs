using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public record OrderConfirmed : IIntegrationEvent
{
    public Guid OrderId { get; set; }
}