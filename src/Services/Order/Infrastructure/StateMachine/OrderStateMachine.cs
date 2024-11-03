using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using cShop.Contracts.Services.Payment;
using MassTransit;

namespace Infrastructure.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;
    
    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        _logger = logger;
        Event(() => OrderSubmitted, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => MakeOrderValidate, c => c.CorrelateById(x => x.Message.OrderId));
        
        Event(() => BasketCheckoutSuccess, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => BasketCheckoutFail, c => c.CorrelateById(x => x.Message.OrderId));
        
        Event(() => PaymentProcessSuccess, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => PaymentProcessFail, c => c.CorrelateById(x => x.Message.OrderId));
        
        Event(() => OrderCanceled, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderConfirmed, c => c.CorrelateById(x => x.Message.OrderId));
        
        InstanceState(e => e.CurrentState);
        
        
        Initially(
            When(OrderSubmitted)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation($"Order submitted {context.Message.OrderId}");
                    context.Saga.UserId = context.Message.UserId;

                    await SendAuditLog();
                })
                .Produce(context => context.Init<MakeOrderValidate>(new
                {
                    OrderId = context.Saga.CorrelationId,
                    context.Message.UserId
                }))
                .TransitionTo(Submitted)
        );
        
        During(Submitted,
            Ignore(OrderSubmitted),
            When(BasketCheckoutSuccess).ThenAsync(async context =>
            {
                _logger.LogInformation($"Order validated {context.Saga.CorrelationId}");

                await SendAuditLog();
            }).TransitionTo(Process),
            When(BasketCheckoutFail).ThenAsync(async context =>
                {
                    _logger.LogInformation("Order checkout failed");

                    await SendAuditLog();
                }).TransitionTo(Cancel)
        );
        During(Process,
            Ignore(OrderSubmitted),
            Ignore(BasketCheckoutSuccess),
            When(PaymentProcessSuccess)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Payment processing success");
                    await SendAuditLog();
                })
                .Produce(
                    context => context.Init<OrderConfirmed>(new {OrderId = context.Saga.CorrelationId, context.Saga.TransactionId}))
                .TransitionTo(Complete),
            When(PaymentProcessFail).Produce(context => context.Init<OrderCancelled>(new {context.Saga.CorrelationId})).TransitionTo(Cancel)
        );
        
        During(Complete,
            Ignore(PaymentProcessSuccess),
            When(OrderConfirmed)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Order confirmed");
                    await SendAuditLog();
                }).Finalize()
            );
        During(Cancel, 
            Ignore(BasketCheckoutFail), 
            Ignore(PaymentProcessFail), 
            When(OrderCanceled)
                .ThenAsync(async context =>
            {
                
                _logger.LogInformation($"Order cancelled {context.Saga.CorrelationId}");
                await SendAuditLog();
            }).Finalize());
        SetCompletedWhenFinalized();
    }
    
    
    
    public State Submitted { get; private set; }
    public State Process { get; private set; }
    public State Complete { get; private set; }
    public State Cancel { get; private set; }
    
    public Event<OrderSubmitted> OrderSubmitted { get; private set; } = null!;
    public Event<MakeOrderValidate> MakeOrderValidate { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutSuccess> BasketCheckoutSuccess { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutFail> BasketCheckoutFail { get; private set; } = null!;
    public Event<OrderCancelled> OrderCanceled { get; private set; } = null!;
    public Event<PaymentProcessSuccess> PaymentProcessSuccess { get; private set; } = null!;
    public Event<PaymentProcessFail> PaymentProcessFail { get; private set; } = null!;
    public Event<OrderConfirmed> OrderConfirmed { get; private set; } = null!;


    public async Task SendAuditLog()
    {
        await Task.CompletedTask;
    }
}