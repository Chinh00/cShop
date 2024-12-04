namespace IntegrationEvents;

public interface PaymentProcessFailIntegrationEvent
{
    public Guid OrderId { get; set; }
}