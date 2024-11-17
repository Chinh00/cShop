using MassTransit;
using MassTransit.Mediator;

namespace Infrastructure.Consumers;

public class OrderPaidIntegrationEventConsumer(IMediator mediator) : IConsumer<OrderPaidIntegrationEventConsumer>
{

    public async Task Consume(ConsumeContext<OrderPaidIntegrationEventConsumer> context)
    {
        await mediator.Publish(context.Message);
    }
}