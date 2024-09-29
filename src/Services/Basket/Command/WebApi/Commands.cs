using cShop.Contracts.Services.Basket;
using cShop.Core.Domain;
using Domain.Entities;

namespace WebApi;

public static class Commands
{
    public record CreateBasket()
    {
        public static Command.CreateBasket Command(Guid userId) => new (userId);
    }
}