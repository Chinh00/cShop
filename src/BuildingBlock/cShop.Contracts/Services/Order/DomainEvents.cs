using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public static class DomainEvents
{
    public interface OrderSubmitted : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    };

    public interface MakeOrderValidate : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
    }
    
    public interface OrderCancelled : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
    }

    public interface OrderConfirmed : IIntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid TransactionId { get; set; }
    }

    
    
}