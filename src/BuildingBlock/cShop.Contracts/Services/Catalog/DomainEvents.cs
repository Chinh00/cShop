using cShop.Core.Domain;

namespace cShop.Contracts.Services.Catalog;

public static class DomainEvents
{
    public record CatalogCreated(Guid Id, string Name, int Quantity, decimal Price, string ImageUrl, Guid? CategoryId, bool IsActive, long Version) : Message, IDomainEvent;
    public record CatalogActivated(Guid Id, long Version) : Message, IDomainEvent;
    public record CatalogInactivated(Guid Id, long Version) : Message, IDomainEvent;
    public record CategoryAssigned(Guid Id, long Version) : Message, IDomainEvent;
}