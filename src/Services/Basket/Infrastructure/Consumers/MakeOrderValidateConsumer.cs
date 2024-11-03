using cShop.Contracts.Services.Basket;
using cShop.Contracts.Services.Order;
using cShop.Infrastructure.Cache.Redis;
using Domain.Entities;
using MassTransit;

namespace Infrastructure.Consumers;

public class MakeOrderValidateConsumer(ILogger<MakeOrderValidateConsumer> logger, IRedisService redisService, ITopicProducer<IntegrationEvent.BasketCheckoutSuccess> orderCheckoutSuccess, ITopicProducer<IntegrationEvent.BasketCheckoutFail> orderCheckoutFailTopic) : IConsumer<MakeOrderValidate>
{
    
    public async Task Consume(ConsumeContext<MakeOrderValidate> context)
    {
        logger.LogInformation($"MakeOrderValidateConsumer received message {context.Message.UserId}");
        var basket = await redisService.HashGetAsync<Basket>(nameof(Basket), context.Message.UserId.ToString(), default);

        if (basket is null) await orderCheckoutFailTopic.Produce(new { OrderId = context.Message.OrderId });
        else
        {
            await redisService.HashRemoveAsync(nameof(Basket), context.Message.UserId.ToString(), default);
            await orderCheckoutSuccess.Produce(new { OrderId = context.Message.OrderId });
        }
        
    }
}