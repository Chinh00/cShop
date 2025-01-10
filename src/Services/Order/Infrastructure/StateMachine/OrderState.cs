using IntegrationEvents;
using MassTransit;

namespace Infrastructure.StateMachine;

public class OrderState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public Guid UserId { get; set; }
    
    public Guid TransactionId { get; set; }
    public string CurrentState { get; set; } = default!;
    
    public DateTime UpdatedTime { get; set; }
    public decimal TotalAmount { get; set; } = default!;

    public int Version { get; set; }

    public List<OrderCheckoutDetail> OrderCheckoutDetails { get; set; } = [];
    
}