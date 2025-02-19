using cShop.Core.Domain;

namespace cShop.Contracts.Services.Catalog;

public static class Command
{
    public record CreateCatalog(string Name, int Quantity, decimal Price, string ImageSrc, Guid? CategoryId) : Message;

    public record ActiveCatalog(Guid CatalogId) : Message;
    public record InActiveCatalog(Guid CatalogId) : Message;
}