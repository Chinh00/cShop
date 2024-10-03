using MassTransit;

namespace Application.StateMachine;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
}