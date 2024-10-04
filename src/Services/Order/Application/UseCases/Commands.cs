using cShop.Contracts.Services.Order;

namespace Application.UseCases;

public class Commands
{
    public record CreateOrder : ICommand<Guid>
    {
        public Guid OrderId { get; init; }
        public List<OrderCreateDetail> OrderCreateDetails { get; init; }

        public record OrderCreateDetail(Guid ProductId, int Quantity);
    };
    public record PaymentOrder(Guid OrderId, Guid TransactionId) : ICommand<Guid>;
    


}