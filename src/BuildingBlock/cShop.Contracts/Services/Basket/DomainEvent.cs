using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Basket;

public static class DomainEvents
{
    public record BasketCreated(Guid BasketId, Guid UserId, long Version) : Message, IDomainEvent;
}