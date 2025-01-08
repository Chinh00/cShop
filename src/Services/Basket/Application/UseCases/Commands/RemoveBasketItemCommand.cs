using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record RemoveBasketItemCommand(Guid ProductId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<RemoveBasketItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
    
    internal class Handler(IRedisService redisService, IClaimContextAccessor claimContextAccessor)
        : IRequestHandler<RemoveBasketItemCommand, IResult>
    {
        public async Task<IResult> Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basket = await redisService.HashGetAsync<Basket>(nameof(Basket),
                claimContextAccessor.GetUserId().ToString(),
                cancellationToken);
            basket.RemoveBasketItem(new BasketItem(){ProductId = request.ProductId});
            await redisService.HashSetAsync(nameof(Basket), claimContextAccessor.GetUserId().ToString(),
                () => Task.FromResult(basket),
                cancellationToken);
            return TypedResults.Ok(ResultModel<Basket>.Create(basket));
        }
    }
}