using cShop.Contracts.Services.Order;
using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateOrderCommand(Guid UserId, List<CreateOrderCommand.OrderItemDetail> Items) : ICommand<IResult>
{
   public record OrderItemDetail(Guid ProductId, int Quantity);
   
   
   public class Validator : AbstractValidator<CreateOrderCommand>
   {
      public Validator()
      {
         RuleFor(x => x.UserId).NotEmpty();
         RuleFor(e => e.Items).NotEmpty().Must(e => e.Count > 0);
      }
   }
   
   internal class Handler(IRepository<Order> orderRepository, ITopicProducer<OrderSubmitted> orderSubmittedProducer)
      : IRequestHandler<CreateOrderCommand, IResult>
   {
      public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
         var order = new Order()
         {
            CustomerId = request.UserId,
            OrderDetails = request.Items.Select(e => new OrderDetail()
            {
               ProductId = e.ProductId,
            }).ToList()
         };
         await orderRepository.AddAsync(order, cancellationToken);
         await orderSubmittedProducer.Produce(new { OrderId = order.Id, UserId = order.CustomerId }, cancellationToken);
         
         return Results.Ok(ResultModel<Order>.Create(order));
      }
   }

}