using Application.UseCases.Dto;
using cShop.Core.Domain;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using MediatR;
namespace Application.UseCases.Command;

public class CreateBasketCommand : ICreateCommand<cShop.Contracts.Services.Basket.Command.CreateBasket,BasketDto>
{
    public cShop.Contracts.Services.Basket.Command.CreateBasket Model { get; }
    
    public class Handler : IRequestHandler<CreateBasketCommand, ResultModel<BasketDto>>
    {
        private readonly IClaimContextAccessor _contextAccessor;

        public Handler(IClaimContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<ResultModel<BasketDto>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = new Basket();
            basket.CreateBasket(request.Model);
            
            foreach (var basketDomainEvent in basket.DomainEvents)
            {
                
            }
            
        }
    }
    
}