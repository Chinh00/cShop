using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using cShop.Contracts.Services.Order;
using cShop.Contracts.Services.Payment;
using cShop.Core.Repository;
using Domain.Outbox;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;
    
    public OrderStateMachine(ILogger<OrderStateMachine> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        Event(() => OrderStartedIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => MakeOrderStockValidateIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockValidatedSuccessIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderStockValidatedFailIntegrationEvent, c => c.CorrelateById(x => x.Message.OrderId));
        
        
        
        Event(() => BasketCheckoutSuccess, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => BasketCheckoutFail, c => c.CorrelateById(x => x.Message.OrderId));
        
        Event(() => PaymentProcessSuccess, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => PaymentProcessFail, c => c.CorrelateById(x => x.Message.OrderId));
        
        Event(() => OrderCanceled, c => c.CorrelateById(x => x.Message.OrderId));
        Event(() => OrderConfirmed, c => c.CorrelateById(x => x.Message.OrderId));
        
        InstanceState(e => Submitted);
        
        
        Initially(
            When(OrderStartedIntegrationEvent)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation($"Order submitted {context.Message.OrderId}");
                    context.Saga.CorrelationId = context.Message.OrderId;
                    context.Saga.UserId = context.Message.UserId;

                    await SendAuditLog();
                })
                .Produce(context => context.Init<MakeOrderStockValidateIntegrationEvent>(new
                {
                    OrderId = context.Saga.CorrelationId,
                    context.Message.UserId,
                    OrderItems = context.Message.OrderDetails
                }))
                .TransitionTo(Validate)
        );
        
        
        
        During(Validate, Ignore(OrderStartedIntegrationEvent), 
            When(OrderStockValidatedSuccessIntegrationEvent).ThenAsync( async (context) =>
            {
                
            }).TransitionTo(PaymentProcess),
            When(OrderStockValidatedFailIntegrationEvent).ThenAsync(async context =>
                {
                    
                }).TransitionTo(Cancel)
            );
        
        
        During(PaymentProcess,
            Ignore(OrderStartedIntegrationEvent),
            Ignore(MakeOrderStockValidateIntegrationEvent),
            Ignore(OrderStockValidatedSuccessIntegrationEvent),
            When(PaymentProcessSuccess)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Payment processing success");
                    await SendAuditLog();
                })
                .Produce(
                    context => context.Init<OrderPaidIntegrationEvent>(
                        new
                        {
                            OrderId = context.Saga.CorrelationId, 
                            CatalogItems = context.Saga.OrderDetails
                        }))
                .TransitionTo(StockProcess),
            When(PaymentProcessFail).Produce(context => context.Init<OrderUnPaidIntegrationEvent>(new {context.Saga.CorrelationId})).TransitionTo(Cancel)
        );
        
        
        During(Complete,
            Ignore(PaymentProcessSuccess),
            When(OrderConfirmed)
                .ThenAsync(async context =>
                {
                    _logger.LogInformation("Order confirmed");
                    using var scope = serviceScopeFactory.CreateScope();
                    var schemaRegistryClient = scope.ServiceProvider.GetService<ISchemaRegistryClient>();
                    var repository = scope.ServiceProvider.GetRequiredService<IRepository<OrderOutbox>>();

                    var serialize = new AvroSerializer<OrderComplete>(schemaRegistryClient,
                        new AvroSerializerConfig() { SubjectNameStrategy = SubjectNameStrategy.Topic });

                    var bytes = await serialize.SerializeAsync(new OrderComplete()
                        {
                            OrderId = context.Saga.CorrelationId.ToString(),
                            UserId = context.Saga.UserId.ToString()
                        },
                        new SerializationContext(MessageComponentType.Value, "order_cdc_events"));
                    await repository.AddAsync(new OrderOutbox()
                    {
                        Id = Guid.NewGuid(),
                        AggregateType = nameof(OrderOutbox),
                        AggregateId = context.Saga.CorrelationId.ToString(),
                        Type = nameof(OrderComplete),
                        Payload = bytes
                    }, default);
                    
                    
                    
                    
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
    
    public State Validate { get; private set; }
    public State PaymentProcess { get; private set; }
    
    public State StockProcess { get; private set; }
    public State Complete { get; private set; }
    
    
    public State Cancel { get; private set; }
    
    public Event<OrderStartedIntegrationEvent> OrderStartedIntegrationEvent { get; private set; } = null!;
    public Event<MakeOrderStockValidateIntegrationEvent> MakeOrderStockValidateIntegrationEvent { get; private set; } = null!;
    public Event<OrderStockValidatedSuccessIntegrationEvent> OrderStockValidatedSuccessIntegrationEvent { get; private set; } = null!;
    public Event<OrderStockValidatedFailIntegrationEvent> OrderStockValidatedFailIntegrationEvent { get; private set; } = null!;
    public Event<BasketCheckoutSuccessIntegrationEvent> BasketCheckoutSuccess { get; private set; } = null!;
    public Event<BasketCheckoutFailIntegrationEvent> BasketCheckoutFail { get; private set; } = null!;
    public Event<OrderCancelled> OrderCanceled { get; private set; } = null!;
    public Event<PaymentProcessSuccess> PaymentProcessSuccess { get; private set; } = null!;
    public Event<PaymentProcessFail> PaymentProcessFail { get; private set; } = null!;
    public Event<OrderConfirmed> OrderConfirmed { get; private set; } = null!;


    async Task SendAuditLog()
    {
        await Task.CompletedTask;
    }
    
    
}