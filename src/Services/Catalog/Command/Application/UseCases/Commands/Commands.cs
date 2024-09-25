using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using MediatR;

namespace Application.UseCases.Commands;

public record Commands
{
    public record CreateCatalog(string Name, float CurrentCost, string ImageSrc, string CategoryId) : IRequest<Guid>
    {
        public Command.CreateCatalog Command(string name, float currentCost, string imageSrc, string categoryId)
        {
            return new Command.CreateCatalog(name, currentCost, imageSrc, Guid.Parse(categoryId));
        }
    }
}