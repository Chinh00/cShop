using cShop.Core.Domain;
using cShop.Infrastructure.Cache.Redis;
using cShop.Infrastructure.IdentityServer;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateBasketCommand(Guid UserId) : ICommand<IResult>
{

    public class Validator : AbstractValidator<CreateBasketCommand>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<CreateBasketCommand, IResult>
    {
        
        private readonly IRedisService _redisService;

        public Handler(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task<IResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _redisService.HashGetOrSetAsync(nameof(Basket),
                request.UserId.ToString(), () => Task.FromResult(new Basket()
                {
                    UserId = request.UserId,
                }), cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(basket.Id));
        }
    }
}