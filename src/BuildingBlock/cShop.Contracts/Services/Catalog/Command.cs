using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Catalog;

public static class Command
{
    public record CreateCatalog(string Name, int Quantity, double Price, string ImageSrc, Guid? CategoryId) : Message;

    public record ActiveCatalog(Guid CatalogId) : Message;
    public record InActiveCatalog(Guid CatalogId) : Message;
}