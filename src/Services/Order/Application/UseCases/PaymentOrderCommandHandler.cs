using cShop.Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases;

public class PaymentOrderCommandHandler : IRequestHandler<Commands.PaymentOrder, ResultModel<Guid>>
{
    private readonly ITopicProducer<IntegrationEvents.PaymentProcessSuccess> _paymentProcessSuccess;
    private readonly ITopicProducer<IntegrationEvents.PaymentProcessFail> _paymentProcessFail;
    private readonly ILogger<PaymentOrderCommandHandler> _logger;

    public PaymentOrderCommandHandler(ITopicProducer<IntegrationEvents.PaymentProcessSuccess> paymentProcessSuccess, ILogger<PaymentOrderCommandHandler> logger, ITopicProducer<IntegrationEvents.PaymentProcessFail> paymentProcessFail)
    {
        _paymentProcessSuccess = paymentProcessSuccess;
        _logger = logger;
        _paymentProcessFail = paymentProcessFail;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.PaymentOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PaymentOrderCommandHandler invoked");
        await _paymentProcessSuccess.Produce(new
        {
            request.OrderId,    
        }, cancellationToken);
        return ResultModel<Guid>.Create(Guid.NewGuid());
    }
}