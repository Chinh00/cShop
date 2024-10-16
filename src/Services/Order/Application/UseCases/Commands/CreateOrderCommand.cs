using cShop.Contracts.Services.Order;
using cShop.Core.Domain;
using cShop.Infrastructure.IdentityServer;
using Domain;
using Infrastructure.Data;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateOrderCommand(Guid OrderId, List<CreateOrderCommand.OrderItemDetail> Items) : ICommand<IResult>
{
   public record OrderItemDetail(Guid ProductId, int Quantity);
   
   
   internal class Handler : IRequestHandler<CreateOrderCommand, IResult>
   {
      private readonly ITopicProducer<OrderSubmitted> _orderCreatedTopic;
      private readonly IClaimContextAccessor _claimContextAccessor;
      private readonly ILogger<CreateOrderCommand> _logger;
      private readonly OrderContext _orderContext;

      public Handler(ITopicProducer<OrderSubmitted> orderCreatedTopic, IClaimContextAccessor claimContextAccessor, ILogger<CreateOrderCommand> logger, OrderContext orderContext)
      {
         _orderCreatedTopic = orderCreatedTopic;
         _claimContextAccessor = claimContextAccessor;
         _logger = logger;
         this._orderContext = orderContext;
      }

      public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
         var userid = _claimContextAccessor.GetUserId();
         _logger.LogInformation("UserId {userId}", userid);

         var order = await _orderContext.Set<Order>().AddAsync(new Order()
         {
            CustomerId = userid,
            OrderDetails = request.Items.Select(e => new OrderDetail()
            {
               ProductId = e.ProductId,
            }).ToList()
         }, cancellationToken);
         await _orderContext.SaveChangesAsync(cancellationToken);
        
         await _orderCreatedTopic.Produce(new {request.OrderId, userid}, cancellationToken);
    
         return Results.Ok(ResultModel<Guid>.Create(order.Entity.Id));
      }
   }

}