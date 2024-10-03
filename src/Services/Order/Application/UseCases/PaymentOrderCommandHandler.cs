using cShop.Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases;

public class PaymentOrderCommandHandler : IRequestHandler<Commands.PaymentOrder, ResultModel<Guid>>
{
    private readonly ITopicProducer<IntegrationEvents.PaymentProcessSuccess> _paymentProcessSuccess;
    private readonly ILogger<PaymentOrderCommandHandler> _logger;

    public PaymentOrderCommandHandler(ITopicProducer<IntegrationEvents.PaymentProcessSuccess> paymentProcessSuccess, ILogger<PaymentOrderCommandHandler> logger)
    {
        _paymentProcessSuccess = paymentProcessSuccess;
        _logger = logger;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.PaymentOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PaymentOrderCommandHandler invoked");
        await _paymentProcessSuccess.Produce(new
        {
            OrderId = request.OrderId,
            TransactionId = Guid.NewGuid(),
            DateTime.Now
        }, cancellationToken);
        return ResultModel<Guid>.Create(Guid.NewGuid());
    }
}