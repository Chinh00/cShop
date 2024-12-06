using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Infrastructure.Consumers;

public class MakeStockValidateConsumer(ILogger<MakeStockValidateConsumer> logger, IServiceProvider scopeFactory) : IConsumer<MakeOrderStockValidateIntegrationEvent>
{
    public async Task Consume(ConsumeContext<MakeOrderStockValidateIntegrationEvent> context)
    {
        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        logger.LogInformation("Stock Validate");
        await mediator.Publish(context.Message, default);
    }
}