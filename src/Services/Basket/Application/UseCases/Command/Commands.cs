using cShop.Core.Domain;

namespace Application.UseCases.Command;

public static class Commands
{
    public record CreateBasket : ICommand<Guid>
    {
        public cShop.Contracts.Services.Basket.Command.CreateBasket Command(Guid userId) => new(userId);
    }
    
    public record AddBasketItem(Guid BasketId, Guid ProductId, int Quantity) : ICommand<Guid>
    {
        public cShop.Contracts.Services.Basket.Command.AddBasketItem Command(Guid basketId, Guid userId, Guid productId, int quantity, double price) => new(basketId, userId, productId, quantity, price);
    }
    
    
}