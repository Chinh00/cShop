using Application.Abstraction;
using Application.UseCases.Specs;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using cShop.Infrastructure.Models;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record PaymentResultCommand(IQueryCollection QueryString) : ICommand<IResult>
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
        IRepository<OrderInfo> repository,
        IVnpayService vnpayService)
        : IRequestHandler<PaymentResultCommand, IResult>
    {
        public async Task<IResult> Handle(PaymentResultCommand request, CancellationToken cancellationToken)
        {
            request.QueryString.TryGetValue(nameof(PaymentResult.vnp_TxnRef), out var txnRef);
            var spec = new GetOrderInfoByTxnRefSpec(int.Parse(txnRef));
            var orderInfo = await repository.FindOneAsync(spec, cancellationToken);
            orderInfo.Status = PaymentStatus.Success;
            
            await repository.UpdateAsync(orderInfo, cancellationToken);
            await topicProducerSuccessIntegrationEvent.Produce(new
            {
                orderInfo.OrderId,
                TransactionId = orderInfo.Id,
            }, cancellationToken);
            return Results.Ok();
        }
    }


}