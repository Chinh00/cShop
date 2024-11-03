using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using GrpcServices;
using MediatR;

namespace Application.UseCases.Commands;

public record AddBasketItemCommand(Guid UserId, Guid BasketId, Guid ProductId) : ICommand<IResult>
{

    
    internal class Handler(IRedisService redisService, Catalog.CatalogClient catalogClient)
        : IRequestHandler<AddBasketItemCommand, IResult>
    {
        public async Task<IResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var catalog = await catalogClient.getProductByIdAsync(new GetCatalogByIdRequest() { Id = request.ProductId.ToString() }, cancellationToken: cancellationToken);

            if (catalog is null)
            {
                return Results.BadRequest(ResultModel<string>.Create("Catalog Not Found"));
            }
            
            var basket = await redisService.HashGetOrSetAsync(nameof(Basket), request.UserId.ToString(),
                () => Task.FromResult(new Basket()
                {
                    UserId = request.UserId
                }), cancellationToken);
            
               basket.AddBasketItem(new BasketItem()
               {
                   BasketId = basket.Id,
                   ProductId = request.ProductId,
               });
               await redisService.HashSetAsync(nameof(Basket), request.UserId.ToString(),
                   () => Task.FromResult(basket), cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(basket.Id));

        }
    }
    
}