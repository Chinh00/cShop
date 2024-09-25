using cShop.Core.Domain;

namespace cShop.Contracts.Services.Basket;

public static class Command
{
    public record CreateBasket(Guid UserId) : Message;
};