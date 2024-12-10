using IntegrationEvents;
using MassTransit;

namespace Infrastructure.StateMachine;

public class ShippingStateMachine : MassTransitStateMachine<ShippingState>
{
    public ShippingStateMachine()
    {
        Event(() => ShipmentCreated, configurator => configurator.CorrelateById(t => t.Message.OrderId));
        Event(() => ShipmentPicked, configurator => configurator.CorrelateById(t => t.Message.OrderId));

        Event(() => ShipmentDelivery, configurator => configurator.CorrelateById(t => t.Message.OrderId));

        InstanceState(e => e.CurrentState);
        
        Initially(When(ShipmentCreated).ThenAsync(async (context) =>
        {
            context.Saga.OrderId = context.Message.OrderId;
        }).TransitionTo(Dispatch));

        During(Dispatch, When(ShipmentPicked).ThenAsync(async (context) =>
        {
            
        }).TransitionTo(Delivery));
        
        During(Delivery, When(ShipmentDelivery).ThenAsync(async (context) =>
        {
            
        }).Produce(context => context.Init<OrderCompleteIntegrationEvent>(new {context.Saga.OrderId})).Finalize());
        
        SetCompletedWhenFinalized();
    }
    
    public State Dispatch { get; set; }
    public State Delivery { get; set; }

    public State Cancel { get; set; }

    public Event<ShipmentCreatedIntegrationEvent> ShipmentCreated { get; set; } = null!;
    public Event<ShipmentPickedIntegrationEvent> ShipmentPicked { get; set; } = null!;
    public Event<ShipmentDeliveryIntegrationEvent> ShipmentDelivery { get; set; } = null!;

}