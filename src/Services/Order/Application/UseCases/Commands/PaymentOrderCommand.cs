using cShop.Core.Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record PaymentOrderCommand(Guid UserId, Guid OrderId, Guid TransactionId) : ICommand<IResult>
{
    
    public class Validator : AbstractValidator<PaymentOrderCommand>
    {
        public Validator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.OrderId).NotEmpty();
            RuleFor(p => p.TransactionId).NotEmpty();
        }
    }
    
    
    internal record Handler(
        ITopicProducer<PaymentProcessSuccessIntegrationEvent> PaymentProcessSuccess,
        ITopicProducer<PaymentProcessFailIntegrationEvent> PaymentProcessFailTopic)
        : IRequestHandler<PaymentOrderCommand, IResult>
    {

        public async Task<IResult> Handle(PaymentOrderCommand request, CancellationToken cancellationToken)
        {
            await PaymentProcessSuccess.Produce(new { request.UserId, request.OrderId, request.TransactionId }, cancellationToken);
            return TypedResults.Ok(request.OrderId);
        }
    }
}