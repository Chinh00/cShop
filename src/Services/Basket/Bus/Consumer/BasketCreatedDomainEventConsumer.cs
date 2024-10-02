using cShop.Contracts.Services.Basket;
using cShop.Infrastructure.Projection;
using MassTransit;
using MediatR;
using Projection;

namespace Bus.Consumer;

public class BasketCreatedDomainEventConsumer : IConsumer<DomainEvents.BasketCreated>
{
    private readonly ILogger<BasketCreatedDomainEventConsumer> _logger;
    private readonly IProjectionRepository<BasketProjection> _repository;
    
    
    public BasketCreatedDomainEventConsumer(ILogger<BasketCreatedDomainEventConsumer> logger, IProjectionRepository<BasketProjection> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.BasketCreated> context)
    {
        await _repository.ReplaceOrInsertEventAsync(new BasketProjection()
        {
            Id = context.Message.BasketId,
            UserId = context.Message.UserId,
            Version = context.Message.Version
        }, projection => projection.Id == context.Message.BasketId && context.Message.Version > projection.Version, default);
    }
}