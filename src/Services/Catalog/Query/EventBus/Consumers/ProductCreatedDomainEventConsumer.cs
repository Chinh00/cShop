using cShop.Contracts.Events.DomainEvents;
using MassTransit;
using MediatR;

namespace EventBus.Consumers;

public class ProductCreatedDomainEventConsumer : IConsumer<ProductCreatedDomainEvent>
{
    private readonly IMediator _mediator;

    public ProductCreatedDomainEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<ProductCreatedDomainEvent> context) =>
        await _mediator.Publish(context.Message);
}