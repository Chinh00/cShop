using cShop.Contracts.Services.Order;

namespace Application.UseCases;

public class Commands
{
    public record CreateOrder(Guid OrderId, DateTime OrderDate) : ICommand<Guid>;
    public record PaymentOrder(Guid OrderId, Guid TransactionId) : ICommand<Guid>;
    


}