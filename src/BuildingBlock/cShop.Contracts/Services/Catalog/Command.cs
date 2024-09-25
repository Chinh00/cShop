using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Services.Catalog;

public static class Command
{
    public record CreateCatalog(string Name, string Price, string Quantity, Guid CategoryId) : Message;
}