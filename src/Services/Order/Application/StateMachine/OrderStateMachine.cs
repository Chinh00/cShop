using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using MassTransit;
using DomainEvents = cShop.Contracts.Services.Order.DomainEvents;

namespace Application.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
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
                    context.Instance.UpdatedTime = context.Message.CreateAt;
                })
                .Produce(context => context.Init<DomainEvents.MakeOrderValidate>(new
                {
                    OrderId = context.Message.OrderId,
                }))
                .TransitionTo(Submitted)
        );
        During(Submitted,
            Ignore(OrderSubmitted),
            When(BasketCheckoutSuccess).ThenAsync(async context =>
            {
                
            }).TransitionTo(Process),
            When(BasketCheckoutFail).ThenAsync(async context =>
                {
                    
                }).TransitionTo(Cancel)
        );
        During(Process,
            Ignore(OrderSubmitted),
            Ignore(BasketCheckoutSuccess),
            When(PaymentProcessSuccess).Produce(context => context.Init<DomainEvents.OrderConfirmed>(new {context.Message.OrderId})).TransitionTo(Complete),
            When(PaymentProcessFail).Produce(context => context.Init<DomainEvents.OrderCancelled>(new {context.Message.OrderId})).TransitionTo(Cancel)
        );
        
        During(Complete,
            Ignore(PaymentProcessSuccess),
            When(OrderConfirmed).ThenAsync(async context => {}).Finalize()
            );
        During(Cancel, Ignore(BasketCheckoutFail), Ignore(PaymentProcessFail), When(OrderCanceled).ThenAsync(
            async context =>
            {
                
            }).Finalize());
        
        SetCompletedWhenFinalized();
    }
    
    
    
    public State Submitted { get; private set; }
    public State Process { get; private set; }
    public State Complete { get; private set; }
    public State Cancel { get; private set; }
    
    public Event<DomainEvents.OrderSubmitted> OrderSubmitted { get; private set; } = null!;
    public Event<DomainEvents.MakeOrderValidate> MakeOrderValidate { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutSuccess> BasketCheckoutSuccess { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutFail> BasketCheckoutFail { get; private set; } = null!;
    public Event<IntegrationEvents.PaymentProcessSuccess> PaymentProcessSuccess { get; private set; } = null!;
    public Event<IntegrationEvents.PaymentProcessFail> PaymentProcessFail { get; private set; } = null!;
    public Event<DomainEvents.OrderCancelled> OrderCanceled { get; private set; } = null!;
    public Event<DomainEvents.OrderConfirmed> OrderConfirmed { get; private set; } = null!;
}