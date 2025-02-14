using cShop.Core.Domain;
using MassTransit;
using MediatR;

namespace Infrastructure;

public class EventDispatcher(ILogger<EventDispatcher> logger, IServiceProvider serviceProvider)
    : IConsumer<IIntegrationEvent>
{

    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        await mediator.Publish(context.Message);
    }
}