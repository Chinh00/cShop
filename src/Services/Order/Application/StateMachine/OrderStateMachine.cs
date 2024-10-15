using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using MassTransit;
using DomainEvents = cShop.Contracts.Services.Order.DomainEvents;

namespace Application.StateMachine;

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
                    _logger.LogInformation($"Order submitted {context.Saga.CorrelationId}");


                    await SendAuditLog();
                })
                .Produce(context => context.Init<MakeOrderValidate>(new
                {
                    OrderId = context.Saga.CorrelationId,
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
                    context.Saga.TransactionId = context.Message.TransactionId;
                    await SendAuditLog();
                })
                .Produce(
                    context => context.Init<DomainEvents.OrderConfirmed>(new {OrderId = context.Saga.CorrelationId, context.Saga.TransactionId}))
                .TransitionTo(Complete),
            When(PaymentProcessFail).Produce(context => context.Init<DomainEvents.OrderCancelled>(new {context.Saga.CorrelationId})).TransitionTo(Cancel)
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
    public Event<IntegrationEvents.PaymentProcessSuccess> PaymentProcessSuccess { get; private set; } = null!;
    public Event<IntegrationEvents.PaymentProcessFail> PaymentProcessFail { get; private set; } = null!;
    public Event<DomainEvents.OrderCancelled> OrderCanceled { get; private set; } = null!;
    public Event<DomainEvents.OrderConfirmed> OrderConfirmed { get; private set; } = null!;


    public async Task SendAuditLog()
    {
        await Task.CompletedTask;
    }
}