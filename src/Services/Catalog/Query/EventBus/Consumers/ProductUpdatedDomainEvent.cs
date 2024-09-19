using cShop.Contracts.Events.DomainEvents;
using MassTransit;
using MediatR;

namespace EventBus.Consumers;

public class ProductUpdatedDomainEventConsumer : IConsumer<ProductNameUpdatedDomainEvent>
{
    private readonly IMediator _mediator;

    public ProductUpdatedDomainEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductNameUpdatedDomainEvent> context) => await _mediator.Publish(context.Message);
}