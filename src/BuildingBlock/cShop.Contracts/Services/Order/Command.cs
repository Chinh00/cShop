using cShop.Core.Domain;

namespace cShop.Contracts.Services.Order;

public class Command
{
    public record CreateOrder(Guid OrderId, Guid UserId) : Message;
}