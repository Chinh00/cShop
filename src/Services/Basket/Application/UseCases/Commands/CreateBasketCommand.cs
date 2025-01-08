using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateBasketCommand : ICommand<IResult>
{

    public class Validator : AbstractValidator<CreateBasketCommand>
    {
        public Validator()
        {
        }
    }
    
    public class Handler(IRedisService redisService, IClaimContextAccessor claimContextAccessor)
        : IRequestHandler<CreateBasketCommand, IResult>
    {

        public async Task<IResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await redisService.HashGetOrSetAsync(nameof(Basket),
                claimContextAccessor.GetUserId().ToString(), () => Task.FromResult(new Basket()
                {
                    UserId = claimContextAccessor.GetUserId(),
                }), cancellationToken);
            return Results.Ok(ResultModel<Basket>.Create(basket));
        }
    }
}