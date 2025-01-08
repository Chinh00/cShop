using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using FluentValidation;
using GrpcServices;
using MediatR;

namespace Application.UseCases.Commands;

public record AddBasketItemCommand(Guid ProductId) : ICommand<IResult>
{

    public class Validator : AbstractValidator<AddBasketItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
    
    
    internal class Handler(
        IRedisService redisService,
        Catalog.CatalogClient catalogClient,
        IClaimContextAccessor claimContextAccessor)
        : IRequestHandler<AddBasketItemCommand, IResult>
    {
        public async Task<IResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var catalog = await catalogClient.getProductByIdAsync(
                new GetCatalogByIdRequest() { Id = request.ProductId.ToString() },
                cancellationToken: cancellationToken);

            if (catalog is null)
            {
                return Results.BadRequest(ResultModel<string>.Create("Catalog Not Found"));
            }
            
            var basket = await redisService.HashGetOrSetAsync(nameof(Basket),
                claimContextAccessor.GetUserId().ToString(),
                () => Task.FromResult(new Basket()
                {
                    UserId = claimContextAccessor.GetUserId(),
                }), cancellationToken);
            
               basket.AddBasketItem(new BasketItem()
               {
                   BasketId = basket.Id,
                   ProductId = request.ProductId,
               });
               await redisService.HashSetAsync(nameof(Basket), claimContextAccessor.GetUserId().ToString(),
                   () => Task.FromResult(basket), cancellationToken);
            return Results.Ok(ResultModel<Basket>.Create(basket));

        }
    }
    
}