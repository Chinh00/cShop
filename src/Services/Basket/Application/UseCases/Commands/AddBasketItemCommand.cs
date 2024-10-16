using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using GrpcServices;
using MediatR;

namespace Application.UseCases.Commands;

public record AddBasketItemCommand(Guid BasketId, Guid ProductId) : ICommand<IResult>
{

    
    internal class Handler(IRedisService redisService, IClaimContextAccessor claimContextAccessor, Catalog.CatalogClient catalogClient)
        : IRequestHandler<AddBasketItemCommand, IResult>
    {
        public async Task<IResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var catalog = await catalogClient.getProductByIdAsync(new GetCatalogByIdRequest() { Id = request.ProductId.ToString() }, cancellationToken: cancellationToken);

            if (catalog is null)
            {
                return Results.BadRequest(ResultModel<string>.Create("Catalog Not Found"));
            }
            
            var basket = await redisService.HashGetOrSetAsync(nameof(Basket), claimContextAccessor.GetUserId().ToString(),
                () => Task.FromResult(new Basket()
                {
                    UserId = claimContextAccessor.GetUserId()
                }), cancellationToken);
            
               basket.AddBasketItem(new BasketItem()
               {
                   BasketId = basket.Id,
                   ProductId = request.ProductId,
               });
               await redisService.HashSetAsync(nameof(Basket), claimContextAccessor.GetUserId().ToString(),
                   () => Task.FromResult(basket), cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(basket.Id));

        }
    }
    
}