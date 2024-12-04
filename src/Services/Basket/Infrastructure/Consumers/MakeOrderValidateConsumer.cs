using cShop.Infrastructure.Cache.Redis;
using Domain.Entities;
using IntegrationEvents;
using MassTransit;

namespace Infrastructure.Consumers;

public class MakeOrderValidateConsumer(ILogger<MakeOrderValidateConsumer> logger, IRedisService redisService, ITopicProducer<BasketCheckoutSuccessIntegrationEvent> orderCheckoutSuccess, ITopicProducer<BasketCheckoutFailIntegrationEvent> orderCheckoutFailTopic) : IConsumer<MakeOrderStockValidateIntegrationEvent>
{
    
    public async Task Consume(ConsumeContext<MakeOrderStockValidateIntegrationEvent> context)
    {
      
        
    }
}