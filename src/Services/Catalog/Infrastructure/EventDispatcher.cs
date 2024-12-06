using cShop.Core.Domain;
using MassTransit;
using MediatR;

namespace Infrastructure;

public class EventDispatcher(IMediator mediator, ILogger<EventDispatcher> logger)
    : IConsumer<IIntegrationEvent>
{

    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        await mediator.Publish(context.Message);
        logger.LogInformation($"Event {context.Message.GetType().Name} published");
    }
}