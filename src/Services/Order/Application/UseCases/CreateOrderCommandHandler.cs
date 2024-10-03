using cShop.Contracts.Services.Order;
using cShop.Infrastructure.IdentityServer;
using MassTransit;

namespace Application.UseCases;

public class CreateOrderCommandHandler : IRequestHandler<Commands.CreateOrder, ResultModel<Guid>>
{

    private readonly ITopicProducer<DomainEvents.OrderSubmitted> _orderCreatedTopic;
    private readonly IClaimContextAccessor _claimContextAccessor;

    public CreateOrderCommandHandler(ITopicProducer<DomainEvents.OrderSubmitted> orderCreatedTopic, IClaimContextAccessor claimContextAccessor)
    {
        _orderCreatedTopic = orderCreatedTopic;
        this._claimContextAccessor = claimContextAccessor;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.CreateOrder request, CancellationToken cancellationToken)
    {
        await _orderCreatedTopic.Produce(new
        {
            request.OrderId,
            UserId = _claimContextAccessor.GetUserId(),
            request.OrderDate
        }, cancellationToken);
        return ResultModel<Guid>.Create(Guid.NewGuid());
    }
}