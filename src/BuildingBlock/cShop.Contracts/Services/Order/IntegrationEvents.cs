using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public static class IntegrationEvents
{
    public interface PaymentProcessSuccess : IIntegrationEvent
    {
        public Guid OrderId { get; }
        public Guid TransactionId { get; }
    }
    public interface PaymentProcessFail : IIntegrationEvent
    {
        public Guid OrderId { get; }
    }
    
   
}