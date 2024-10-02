using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Basket;

public static class Command
{
    public record CreateBasket(Guid UserId) : Message;

    public record AddBasketItem(Guid BasketId, Guid UserId, Guid ProductId, int Quantity, double Price)
        : Message;
    
    
    
}