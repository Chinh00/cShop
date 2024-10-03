using cShop.Contracts.Services.Order;

namespace Application.UseCases;

public class Commands
{
    public record CreateOrder(Guid OrderId, DateTime OrderDate) : ICommand<Guid>
    {
        public Command.CreateOrder Command (Guid orderId, Guid userId) => new (orderId, userId);
    }
}