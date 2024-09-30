using cShop.Core.Domain;

namespace Application.UseCases.Command;

public static class Commands
{
    public record CreateBasket : ICommand<Guid>
    {
        public cShop.Contracts.Services.Basket.Command.CreateBasket Command(Guid userId) => new(userId);
    }
}