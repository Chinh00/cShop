using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateBasketCommand : ICommand<IResult>
{
    

    public class Handler : IRequestHandler<CreateBasketCommand, IResult>
    {
        
        private readonly IRedisService _redisService;
        private readonly IClaimContextAccessor _claimContextAccessor;

        public Handler(IRedisService redisService, IClaimContextAccessor claimContextAccessor)
        {
            _redisService = redisService;
            _claimContextAccessor = claimContextAccessor;
        }
        public async Task<IResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _redisService.HashGetOrSetAsync(nameof(Basket),
                _claimContextAccessor.GetUserId().ToString(), () => Task.FromResult(new Basket()
                {
                    UserId = _claimContextAccessor.GetUserId(),
                }), cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(basket.Id));
        }
    }
}