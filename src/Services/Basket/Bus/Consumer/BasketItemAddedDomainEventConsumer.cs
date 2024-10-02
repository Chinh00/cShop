using cShop.Contracts.Services.Basket;
using cShop.Infrastructure.Projection;
using MassTransit;
using MongoDB.Driver;
using Projection;

namespace Bus.Consumer;

public class BasketItemAddedDomainEventConsumer : IConsumer<DomainEvents.BasketItemAdded>
{
    private readonly ILogger<BasketItemAddedDomainEventConsumer> _logger;
    private readonly IProjectionDbContext _projectionDbContext;

    public BasketItemAddedDomainEventConsumer(ILogger<BasketItemAddedDomainEventConsumer> logger, IProjectionDbContext projectionDbContext)
    {
        _logger = logger;
        _projectionDbContext = projectionDbContext;
    }

    public async Task Consume(ConsumeContext<DomainEvents.BasketItemAdded> context)
    {
        _logger.LogInformation("Consume BasketItemAddedDomainEventConsumer");
        var collection = _projectionDbContext.GetCollection<BasketProjection>();

        var filter = Builders<BasketProjection>.Filter.Eq(e => e.Id, context.Message.BasketId);
        var update = Builders<BasketProjection>.Update
            .AddToSet(e => e.Items, new BasketItem(context.Message.ProductId, context.Message.Quantity, context.Message.Price))
            .Set(e => e.Version, context.Message.Version);
        await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

    }
}