using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;
using OrderDetail = Domain.OrderDetail;

namespace Application.UseCases.Commands;

public record CreateOrderCommand(
   Guid UserId, 
   List<CreateOrderCommand.OrderItemDetail> Items,
   DateTime OrderDate) : ICommand<IResult>
{
   public record OrderItemDetail(Guid ProductId, int Quantity);
   
   
   public class Validator : AbstractValidator<CreateOrderCommand>
   {
      public Validator()
      {
         RuleFor(x => x.UserId).NotEmpty();
         RuleFor(x => x.OrderDate).NotNull();
         RuleFor(e => e.Items).NotEmpty().Must(e => e.Count > 0);
      }
   }
   
   internal class Handler(IRepository<Order> orderRepository, ITopicProducer<OrderStartedIntegrationEvent> orderStartedTopic)
      : IRequestHandler<CreateOrderCommand, IResult>
   {
      public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
         var order = new Order()
         {
            CustomerId = request.UserId,
            OrderDate = request.OrderDate,
            OrderDetails = request.Items.Select(e => new OrderDetail()
            {
               ProductId = e.ProductId,
               Quantity = e.Quantity
            }).ToList()
         };
         await orderRepository.AddAsync(order, cancellationToken);
         
         await orderStartedTopic.Produce(
            new
            {
               OrderId = order.Id, UserId = order.CustomerId,
               OrderCheckoutDetails =
                  order.OrderDetails.Select(e => new { ProductId = e.ProductId, Quantity = e.Quantity })
            }, cancellationToken);
         
         return Results.Ok(ResultModel<Order>.Create(order));
      }
   }

}