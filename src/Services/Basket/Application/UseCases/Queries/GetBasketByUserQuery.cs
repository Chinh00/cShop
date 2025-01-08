using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetBasketByUserQuery : ICommand<IResult>
{
    
    
    public class Validator : AbstractValidator<GetBasketByUserQuery>
    {
        public Validator()
        {
        }
    }
    
    internal class Handler(IRedisService redisService, IClaimContextAccessor claimContextAccessor) : IRequestHandler<GetBasketByUserQuery, IResult>
    {
        public async Task<IResult> Handle(GetBasketByUserQuery request, CancellationToken cancellationToken)
        {
            var basket = await redisService.HashGetAsync<Basket>(nameof(Basket), claimContextAccessor.GetUserId().ToString(), cancellationToken);
            return TypedResults.Ok(ResultModel<Basket>.Create(basket));

        }
    }
}