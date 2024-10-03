using cShop.Contracts.Services.Basket;
using MassTransit;
using DomainEvents = cShop.Contracts.Services.Order.DomainEvents;

namespace Application.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
        Event(() => OrderSubmitted, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => MakeOrderValidate, c => c.CorrelateById(x => x.Message.OrderId));
        
        
        Initially(
            When(OrderSubmitted)
                .ThenAsync(async context =>
                {
                    
                }).Produce(MakeOrderValidate).TransitionTo(Submitted)
        );
        During(Submitted,
            Ignore(OrderSubmitted),
            When(BasketCheckoutSuccess).ThenAsync(async context =>
            {
                
            }).Produce().TransitionTo(Process),
            When(BasketCheckoutFail).ThenAsync(async context =>
                {
                    
                }).TransitionTo(Cancel)
        );
        During();


    }
    
    
    
    public State Submitted { get; private set; }
    public State Process { get; private set; }
    public State Complete { get; private set; }
    public State Cancel { get; private set; }
    
    public Event<DomainEvents.OrderSubmitted> OrderSubmitted { get; private set; } = null!;
    public Event<DomainEvents.MakeOrderValidate> MakeOrderValidate { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutSuccess> BasketCheckoutSuccess { get; private set; } = null!;
    public Event<IntegrationEvent.BasketCheckoutFail> BasketCheckoutFail { get; private set; } = null!;
}