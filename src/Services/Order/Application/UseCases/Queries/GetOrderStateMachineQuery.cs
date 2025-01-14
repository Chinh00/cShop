using cShop.Infrastructure.Mongodb;
using FluentValidation;
using Infrastructure.StateMachine;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Application.UseCases.Queries;

public record GetOrderStateMachineQuery(Guid OrderId) : IRequest<IResult>
{
    public class Validator : AbstractValidator<GetOrderStateMachineQuery>
    {
        
    }
    
    internal class Handler : IRequestHandler<GetOrderStateMachineQuery, IResult>
    {
        private readonly ILogger<GetOrderStateMachineQuery> _logger;
        private readonly IMongoCollection<OrderState> _orderStateCollection;
        
        public Handler(ILogger<GetOrderStateMachineQuery> logger, IOptions<MongoDbbOption> options)
        {
            _logger = logger;
            _orderStateCollection = new MongoClient(options.Value.ToString()).GetDatabase(options.Value.DatabaseName)
                .GetCollection<OrderState>("OrderSaga");
        }

        public async Task<IResult> Handle(GetOrderStateMachineQuery request, CancellationToken cancellationToken)
        {
            
            var orderInfo = await _orderStateCollection.Find(c => c.CorrelationId == request.OrderId).ToListAsync(cancellationToken: cancellationToken);

            return Results.Ok(orderInfo.FirstOrDefault());
        }
    }
}