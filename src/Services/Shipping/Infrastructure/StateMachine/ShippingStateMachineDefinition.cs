using MassTransit;

namespace Infrastructure.StateMachine;

public class ShippingStateMachineDefinition : SagaDefinition<ShippingState>
{
    public ShippingStateMachineDefinition()
    {
        ConcurrentMessageLimit = 10;
    }

    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<ShippingState> sagaConfigurator,
        IRegistrationContext context)
    {
        base.ConfigureSaga(endpointConfigurator, sagaConfigurator, context);
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 5000, 10000));
        endpointConfigurator.UseInMemoryOutbox();
    }
} 