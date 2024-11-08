using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record RemoveBasketItemCommand(Guid UserId, Guid ProductId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<RemoveBasketItemCommand>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
    
    internal class Handler(IRedisService redisService) : IRequestHandler<RemoveBasketItemCommand, IResult>
    {

        public async Task<IResult> Handle(RemoveBasketItemCommand request, CancellationToken cancellationToken)
        {
            var redis = await redisService.HashGetAsync<Basket>(nameof(Basket), request.UserId.ToString(), cancellationToken);
            redis.RemoveBasketItem(new BasketItem(){ProductId = request.ProductId});
            await redisService.HashSetAsync(nameof(Basket), request.UserId.ToString(), () => Task.FromResult(redis), cancellationToken);
            return TypedResults.Ok(ResultModel<Basket>.Create(redis));
        }
    }
}