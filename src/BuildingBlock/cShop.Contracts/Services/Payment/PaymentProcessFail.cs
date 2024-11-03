namespace cShop.Contracts.Services.Payment;

public interface PaymentProcessFail
{
    public Guid OrderId { get; set; }
}