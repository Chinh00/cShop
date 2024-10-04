using cShop.Contracts.Services.Order;
using cShop.Infrastructure.IdentityServer;
using Domain;
using Infrastructure.Data;
using MassTransit;

namespace Application.UseCases;

public class CreateOrderCommandHandler : IRequestHandler<Commands.CreateOrder, ResultModel<Guid>>
{

    private readonly ITopicProducer<DomainEvents.OrderSubmitted> _orderCreatedTopic;
    private readonly IClaimContextAccessor _claimContextAccessor;
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly OrderContext orderContext;
    public CreateOrderCommandHandler(ITopicProducer<DomainEvents.OrderSubmitted> orderCreatedTopic, IClaimContextAccessor claimContextAccessor, ILogger<CreateOrderCommandHandler> logger, OrderContext orderContext)
    {
        _orderCreatedTopic = orderCreatedTopic;
        this._claimContextAccessor = claimContextAccessor;
        _logger = logger;
        this.orderContext = orderContext;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.CreateOrder request, CancellationToken cancellationToken)
    {
        var userid = _claimContextAccessor.GetUserId();
        _logger.LogInformation("UserId {userId}", userid);

        var order = await orderContext.Set<Order>().AddAsync(new Order()
        {
            CustomerId = userid,
            OrderDetails = request.OrderCreateDetails.Select(e => new OrderDetail()
            {
                ProductId = e.ProductId,
            }).ToList()
        }, cancellationToken);
        await orderContext.SaveChangesAsync(cancellationToken);
        
        await _orderCreatedTopic.Produce(new {request.OrderId, userid}, cancellationToken);
    
        return ResultModel<Guid>.Create(order.Entity.Id);
    }
}