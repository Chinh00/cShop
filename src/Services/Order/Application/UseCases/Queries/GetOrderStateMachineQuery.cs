using cShop.Contracts.Services.Order;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Application.UseCases.Queries;

public record GetOrderStateMachineQuery(Guid OrderId) : IRequest<IResult>
{
    public class Validator : AbstractValidator<GetOrderStateMachineQuery>
    {
        
    }
    
    internal class Handler : IRequestHandler<GetOrderStateMachineQuery, IResult>
    {
        private readonly ILogger<GetOrderStateMachineQuery> _logger;
        private readonly IRequestClient<CheckOrder> _client;
        public Handler(ILogger<GetOrderStateMachineQuery> logger, IRequestClient<CheckOrder> client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IResult> Handle(GetOrderStateMachineQuery request, CancellationToken cancellationToken)
        {
            
            var orderStatus = await _client.GetResponse<OrderStatus, OrderNotFound>(new { OrderId = request.OrderId },
                cancellationToken
            );

            return Results.Ok(orderStatus);
        }
    }
}