using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Catalog;

public static class DomainEvents
{
    public record CatalogCreated(Guid Id, string Name, int Quantity, double Price, string ImageUrl, Guid CategoryId, bool IsActive, long Version) : Message, IDomainEvent;
    public record CatalogActivated(Guid Id, long Version) : Message, IDomainEvent;
    public record CatalogInactivated(Guid Id, long Version) : Message, IDomainEvent;
}