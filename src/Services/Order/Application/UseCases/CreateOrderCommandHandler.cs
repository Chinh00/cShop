using cShop.Contracts.Services.Order;
using MassTransit;

namespace Application.UseCases;

public class CreateOrderCommandHandler : IRequestHandler<Commands.CreateOrder, ResultModel<Guid>>
{

    private readonly ITopicProducer<DomainEvents.OrderSubmitted> _orderCreatedTopic;

    public CreateOrderCommandHandler(ITopicProducer<DomainEvents.OrderSubmitted> orderCreatedTopic)
    {
        _orderCreatedTopic = orderCreatedTopic;
    }

    public async Task<ResultModel<Guid>> Handle(Commands.CreateOrder request, CancellationToken cancellationToken)
    {
        return ResultModel<Guid>.Create(Guid.NewGuid());
    }
}