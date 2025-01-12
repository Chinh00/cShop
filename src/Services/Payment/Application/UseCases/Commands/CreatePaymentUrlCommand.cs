using Application.UseCases.Specs;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Commands;

public record CreatePaymentUrlCommand(Guid OrderId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreatePaymentUrlCommand>
    {

    }
    internal class Handler(
        IRepository<OrderInfo> orderRepository,
        IClaimContextAccessor contextAccessor) : IRequestHandler<CreatePaymentUrlCommand, IResult>
    {
        public async Task<IResult> Handle(CreatePaymentUrlCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetOrderInfoByIdSpec(request.OrderId, contextAccessor.GetUserId());
            var orderInfo =  await orderRepository.FindOneAsync(spec, cancellationToken);
            
            await orderRepository.UpdateAsync(orderInfo, cancellationToken);
            return Results.Ok();
        }
    }

}