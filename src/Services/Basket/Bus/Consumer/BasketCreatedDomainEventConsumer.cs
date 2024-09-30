using cShop.Contracts.Services.Basket;
using MassTransit;

namespace Bus.Consumer;

public class BasketCreatedDomainEventConsumer : IConsumer<DomainEvents.BasketCreated>
{
    private readonly ILogger<BasketCreatedDomainEventConsumer> _logger;

    public BasketCreatedDomainEventConsumer(ILogger<BasketCreatedDomainEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DomainEvents.BasketCreated> context)
    {
        _logger.LogInformation("BasketCreatedDomainEventConsumer {consumer-message}", context.Message);
    }
}