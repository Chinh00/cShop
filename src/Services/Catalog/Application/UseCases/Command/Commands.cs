using cShop.Core.Domain;

namespace Application.UseCases.Command;

public static class Commands
{
    public record CreateCatalog(string Name, int Quantity, double Price, string ImageSrc, Guid CategoryId) : ICommand<Guid>
    {
        public cShop.Contracts.Services.Catalog.Command.CreateCatalog Command(string name, int quantity, double price, string imageSrc, Guid categoryId)
        {
            return new (name, quantity, price, imageSrc, categoryId);
        }
    }


    public record ActiveCatalog(Guid Id) : ICommand<Guid>
    {
        public cShop.Contracts.Services.Catalog.Command.ActiveCatalog Command(Guid catalogId) => new(catalogId);
    }
    
    public record InActiveCatalog(Guid Id) : ICommand<Guid>
    {
        public cShop.Contracts.Services.Catalog.Command.ActiveCatalog Command(Guid catalogId) => new(catalogId);
    }
    
    
}