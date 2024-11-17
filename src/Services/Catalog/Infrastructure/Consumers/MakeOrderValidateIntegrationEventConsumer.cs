using cShop.Contracts.Services.Order;
using MassTransit;
using MediatR;

namespace Infrastructure.Consumers;

public class MakeOrderValidateIntegrationEventConsumer(IMediator mediator)
    : IConsumer<MakeOrderStockValidateIntegrationEvent>
{
    public async Task Consume(ConsumeContext<MakeOrderStockValidateIntegrationEvent> context)
    {
        await mediator.Publish(context.Message);
    }
}