using cShop.Core.Domain;

namespace cShop.Contracts.Services.Catalog;

public class IntegrationEvent
{
    public record CatalogCreatedIntegration(Guid Id, string ProductName, double Price) : Message, IIntegrationEvent {}
}