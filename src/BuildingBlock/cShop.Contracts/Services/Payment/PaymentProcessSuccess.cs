namespace cShop.Contracts.Services.Payment;

public interface PaymentProcessSuccess
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TransactionId { get; set; }
}