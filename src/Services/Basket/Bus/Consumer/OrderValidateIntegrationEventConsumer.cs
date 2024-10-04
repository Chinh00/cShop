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
    private readonly ILogger<OrderValidateIntegrationEventConsumer> _logger;
    
    public OrderValidateIntegrationEventConsumer(ITopicProducer<IntegrationEvent.BasketCheckoutSuccess> orderCheckoutSuccess, ITopicProducer<IntegrationEvent.BasketCheckoutFail> orderCheckoutFail, IProjectionRepository<BasketProjection> basketProjectionRepository, ILogger<OrderValidateIntegrationEventConsumer> logger)
    {
        _orderCheckoutSuccess = orderCheckoutSuccess;
        _orderCheckoutFail = orderCheckoutFail;
        _basketProjectionRepository = basketProjectionRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DomainEvents.MakeOrderValidate> context)
    {
        _logger.LogInformation("Consume OrderValidateIntegrationEventConsumer");
        
        var basket = await _basketProjectionRepository.FindByIdAsync(context.Message.OrderId, default);
        if (basket == null)
        {
            _logger.LogInformation("Basket Not Found");
            await _orderCheckoutFail.Produce(new IntegrationEvent.BasketCheckoutFail(context.Message.OrderId));
        } 
        
        
        _logger.LogInformation("Basket Checkout Success");
        await _orderCheckoutSuccess.Produce(new IntegrationEvent.BasketCheckoutSuccess(context.Message.OrderId));
        
    }
}