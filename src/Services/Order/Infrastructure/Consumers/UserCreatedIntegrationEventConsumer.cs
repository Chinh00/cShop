using IntegrationEvents;
using MassTransit;
using MassTransit.Mediator;

namespace Infrastructure.Consumers;

public class UserCreatedIntegrationEventConsumer(IMediator mediator) : IConsumer<UserCreatedIntegrationEvent>
{

    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        await mediator.Publish(context.Message);
    }
}