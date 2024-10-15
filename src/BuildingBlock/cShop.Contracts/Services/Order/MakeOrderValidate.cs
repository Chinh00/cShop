using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public interface MakeOrderValidate : IIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
}