using cShop.Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases.Commands;

public record PaymentOrderCommand(Guid OrderId, Guid TransactionId) : ICommand<IResult>
{
    internal class Handler : IRequestHandler<PaymentOrderCommand, IResult>
    {
        private readonly ITopicProducer<IntegrationEvents.PaymentProcessSuccess> _paymentProcessSuccess;
        private readonly ITopicProducer<IntegrationEvents.PaymentProcessFail> _paymentProcessFail;
        private readonly ILogger<PaymentOrderCommand> _logger;

        public Handler(ITopicProducer<IntegrationEvents.PaymentProcessSuccess> paymentProcessSuccess, ITopicProducer<IntegrationEvents.PaymentProcessFail> paymentProcessFail, ILogger<PaymentOrderCommand> logger)
        {
            _paymentProcessSuccess = paymentProcessSuccess;
            _paymentProcessFail = paymentProcessFail;
            _logger = logger;
        }

        public async Task<IResult> Handle(PaymentOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("PaymentOrderCommandHandler invoked");
            await _paymentProcessSuccess.Produce(new
            {
                request.OrderId,    
            }, cancellationToken);
            return Results.Ok(ResultModel<Guid>.Create(Guid.NewGuid()));
        }
    }
}