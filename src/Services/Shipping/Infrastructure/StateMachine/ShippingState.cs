using MassTransit;

namespace Infrastructure.StateMachine;

public class ShippingState : ISagaVersion, SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public Guid OrderId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; } = null!;
}