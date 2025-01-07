using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Queries;

public record GetBasketByUserQuery(Guid UserId) : ICommand<IResult>
{
    
    
    public class Validator : AbstractValidator<GetBasketByUserQuery>
    {
        public Validator()
        {
            RuleFor(e => e.UserId).NotEmpty();
        }
    }
    
    internal class Handler(IRedisService redisService) : IRequestHandler<GetBasketByUserQuery, IResult>
    {

        public async Task<IResult> Handle(GetBasketByUserQuery request, CancellationToken cancellationToken)
        {
            var basket = await redisService.HashGetAsync<Basket>(nameof(Basket), request.UserId.ToString(), cancellationToken);
            return TypedResults.Ok(ResultModel<Basket>.Create(basket));

        }
    }
}