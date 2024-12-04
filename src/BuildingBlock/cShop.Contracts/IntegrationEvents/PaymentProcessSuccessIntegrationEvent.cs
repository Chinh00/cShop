namespace IntegrationEvents;

public interface PaymentProcessSuccessIntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TransactionId { get; set; }
}