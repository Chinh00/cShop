using cShop.Contracts.Services.Order;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;

    public OrderStateMachine(ILogger<OrderStateMachine> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        Event(() => OrderStartedIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));

        Event(() => MakeOrderStockValidateIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockValidatedSuccessIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockValidatedFailIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));


        Event(() => BasketCheckoutSuccess, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => BasketCheckoutFail, c => c.CorrelateById(x => x.Message.OrderId));

        Event(() => PaymentProcessSuccessIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => PaymentProcessFailIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockChangedIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockUnavailableIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));

        Event(() => OrderCanceled, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderConfirmed, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderCompleteIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));

        InstanceState(e => e.CurrentState);


        Initially(
            When(OrderStartedIntegrationEvent)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation($"Order submitted {context.Message.OrderId}");
                    context.Saga.CorrelationId = context.Message.OrderId;
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.OrderCheckoutDetails = context.Message.OrderCheckoutDetails.ToList();
                    await SendAuditLog();
                })
                .Produce(context => context.Init<MakeOrderStockValidateIntegrationEvent>(new
                {                                                               
                    OrderId = context.Saga.CorrelationId,
                    context.Message.UserId,
                    OrderItems = context.Message.OrderCheckoutDetails
                }))
                .TransitionTo(Validate)
        );


        During(Validate, Ignore(OrderStartedIntegrationEvent),
            When(OrderStockValidatedSuccessIntegrationEvent).ThenAsync(async (context) =>
                {
                    _logger.LogInformation($"Order submitted {context.Message.OrderId}");                    
                })
                .TransitionTo(PaymentProcess),
            When(OrderStockValidatedFailIntegrationEvent).ThenAsync(async context => { }).TransitionTo(Cancel)
        );                  


        During(PaymentProcess,
            Ignore(OrderStartedIntegrationEvent),
            Ignore(MakeOrderStockValidateIntegrationEvent),
            Ignore(OrderStockValidatedSuccessIntegrationEvent),
            When(PaymentProcessSuccessIntegrationEvent)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Payment processing success");
                    await SendAuditLog();
                })
                .Produce(
                    context => context.Init<OrderPaidIntegrationEvent>(
                        new
                        {
                            OrderId = context.Saga.CorrelationId,
                            OrderCheckoutDetails = context.Saga.OrderCheckoutDetails
                        }))
                .TransitionTo(StockProcess),
            When(PaymentProcessFailIntegrationEvent)
                .Produce(context => context.Init<OrderUnPaidIntegrationEvent>(new { context.Saga.CorrelationId }))
                .TransitionTo(Cancel)
        );
        During(StockProcess,
            Ignore(OrderStartedIntegrationEvent),
            Ignore(MakeOrderStockValidateIntegrationEvent),
            Ignore(OrderStockValidatedSuccessIntegrationEvent),
            When(OrderStockChangedIntegrationEvent)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Stock processing success");
                    await SendAuditLog();
                })
                .Produce(
                    context => context.Init<OrderConfirmed>(
                        new
                        {
                            OrderId = context.Saga.CorrelationId,
                        }))
                .TransitionTo(Success),
            When(OrderStockUnavailableIntegrationEvent)
                .Produce(context => context.Init<OrderUnPaidIntegrationEvent>(new { context.Saga.CorrelationId }))
                .TransitionTo(Cancel)
        );


        During(Success,
            Ignore(PaymentProcessSuccessIntegrationEvent),
            When(OrderCompleteIntegrationEvent)
                .ThenAsync(async context => { await SendAuditLog(); }).Finalize()
        );
        During(Cancel,
            Ignore(BasketCheckoutFail),
            Ignore(PaymentProcessFailIntegrationEvent),
            When(OrderCanceled)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation($"Order cancelled {context.Saga.CorrelationId}");
                    await SendAuditLog();
                }).Finalize());

        SetCompletedWhenFinalized();
    }


    public State Submit { get; private set; }

    public State Validate { get; private set; }

    public State PaymentProcess { get; private set; }
    public State StockProcess { get; private set; }

    public State Success { get; private set; }

    public State Cancel { get; private set; }

    public Event<OrderStartedIntegrationEvent> OrderStartedIntegrationEvent { get; private set; } = null!;

    public Event<MakeOrderStockValidateIntegrationEvent> MakeOrderStockValidateIntegrationEvent { get; private set; } =
        null!;

    public Event<OrderStockValidatedSuccessIntegrationEvent> OrderStockValidatedSuccessIntegrationEvent
    {
        get;
        private set;
    } = null!;

    public Event<OrderStockValidatedFailIntegrationEvent>
        OrderStockValidatedFailIntegrationEvent { get; private set; } = null!;

    public Event<BasketCheckoutSuccessIntegrationEvent> BasketCheckoutSuccess { get; private set; } = null!;
    public Event<BasketCheckoutFailIntegrationEvent> BasketCheckoutFail { get; private set; } = null!;
    public Event<OrderStockChangedIntegrationEvent> OrderStockChangedIntegrationEvent { get; private set; } = null!;

    public Event<OrderStockUnavailableIntegrationEvent> OrderStockUnavailableIntegrationEvent { get; private set; } =
        null!;


    public Event<OrderCancelled> OrderCanceled { get; private set; } = null!;

    public Event<PaymentProcessSuccessIntegrationEvent> PaymentProcessSuccessIntegrationEvent { get; private set; } =
        null!;

    public Event<PaymentProcessFailIntegrationEvent> PaymentProcessFailIntegrationEvent { get; private set; } = null!;
    public Event<OrderConfirmed> OrderConfirmed { get; private set; } = null!;

    public Event<OrderCompleteIntegrationEvent> OrderCompleteIntegrationEvent { get; private set; } = null!;

    async Task SendAuditLog()
    {
        await Task.CompletedTask;
    }
}