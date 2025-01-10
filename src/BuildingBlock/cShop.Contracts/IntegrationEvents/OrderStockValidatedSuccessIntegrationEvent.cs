using cShop.Core.Domain;

namespace IntegrationEvents;

public record OrderStockValidatedSuccessIntegrationEvent : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
}
