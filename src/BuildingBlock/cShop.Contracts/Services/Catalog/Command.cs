using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Catalog;

public static class Command
{
    public record CreateCatalog(string Name, float Price, string ImageSrc, Guid CategoryId) : Message;
}