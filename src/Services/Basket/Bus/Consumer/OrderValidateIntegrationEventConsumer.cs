using cShop.Contracts.Services.Basket;
using cShop.Infrastructure.Projection;
using MassTransit;
using Projection;
using DomainEvents = cShop.Contracts.Services.Order.DomainEvents;

namespace Bus.Consumer;

public class OrderValidateIntegrationEventConsumer : IConsumer<DomainEvents.MakeOrderValidate>
{
    private readonly ITopicProducer<IntegrationEvent.BasketCheckoutSuccess> _orderCheckoutSuccess;
    private readonly ITopicProducer<IntegrationEvent.BasketCheckoutFail> _orderCheckoutFail;
    private readonly IProjectionRepository<BasketProjection> _basketProjectionRepository;

    public OrderValidateIntegrationEventConsumer(ITopicProducer<IntegrationEvent.BasketCheckoutSuccess> orderCheckoutSuccess, ITopicProducer<IntegrationEvent.BasketCheckoutFail> orderCheckoutFail, IProjectionRepository<BasketProjection> basketProjectionRepository)
    {
        _orderCheckoutSuccess = orderCheckoutSuccess;
        _orderCheckoutFail = orderCheckoutFail;
        _basketProjectionRepository = basketProjectionRepository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.MakeOrderValidate> context)
    {
        var basket = await _basketProjectionRepository.FindByIdAsync(context.Message.OrderId, default);
        if (basket == null) await _orderCheckoutFail.Produce(new IntegrationEvent.BasketCheckoutFail(context.Message.OrderId));
                    
        await _orderCheckoutSuccess.Produce(new IntegrationEvent.BasketCheckoutSuccess(context.Message.OrderId));
        
    }
}