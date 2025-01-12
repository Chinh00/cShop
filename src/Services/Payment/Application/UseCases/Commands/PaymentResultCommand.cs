using Application.UseCases.Specs;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record PaymentResultCommand(Guid OrderId, Guid TransactionId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<PaymentResultCommand>
    {
        public Validator()
        {
            
        }
    }

    internal class Handler(
        IClaimContextAccessor claimContextAccessor,
        ITopicProducer<PaymentProcessSuccessIntegrationEvent> topicProducerSuccessIntegrationEvent,
        ITopicProducer<PaymentProcessFailIntegrationEvent> topicProducerFailIntegrationEvent,
        IRepository<OrderInfo> repository)
        : IRequestHandler<PaymentResultCommand, IResult>
    {
        public async Task<IResult> Handle(PaymentResultCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetOrderInfoByIdSpec(request.OrderId, claimContextAccessor.GetUserId());
            var orderInfo = await repository.FindOneAsync(spec, cancellationToken);
            orderInfo.TransactionId = request.TransactionId;
            await repository.UpdateAsync(orderInfo, cancellationToken);
            await topicProducerSuccessIntegrationEvent.Produce(new
            {
                request.OrderId,
                TransactionId = orderInfo.TransactionId,
            }, cancellationToken);
            return Results.Ok();
        }
    }


}