using cShop.Contracts.Services.Catalog;
using cShop.Core.Domain;
using MediatR;
using Command = cShop.Contracts.Services.Basket.Command;

namespace Application.UseCases.Commands;

public record Commands
{
    public record CreateCatalog(string Name, float CurrentCost, string ImageSrc, string CategoryId) : IRequest<Guid>
    {
       
    }
}