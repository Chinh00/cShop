using MassTransit;

namespace Application.StateMachine;

public class OrderState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    
    public DateTime UpdatedTime { get; set; }

    public int Version { get; set; }
}