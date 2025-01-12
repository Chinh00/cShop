using cShop.Core.Domain;
using MassTransit;
using MediatR;

namespace Infrastructure;

public sealed class EventDispatcher(IMediator mediator) : IConsumer<IIntegrationEvent>
{
    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        await mediator.Publish(context.Message);        
        await Task.CompletedTask;
    }
}
