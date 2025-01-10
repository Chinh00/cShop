using Application.UseCases.Specs;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateOrderCommand(
   List<CreateOrderCommand.OrderItemDetail> Items,
   DateTime OrderDate) : ICommand<IResult>
{
   public record OrderItemDetail(Guid ProductId, int Quantity);
   
   
   public class Validator : AbstractValidator<CreateOrderCommand>
   {
      public Validator()
      {
         RuleFor(x => x.OrderDate).NotNull();
         RuleFor(e => e.Items).NotEmpty().Must(e => e.Count > 0);
      }
   }
   
   internal class Handler(
      IRepository<Order> orderRepository,
      IListRepository<ProductInfo> productInfoRepository,
      ITopicProducer<OrderStartedIntegrationEvent> orderStartedTopic,
      IClaimContextAccessor contextAccessor)
      : IRequestHandler<CreateOrderCommand, IResult>
   {
      public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
      {
         var listProductInfo = await productInfoRepository.FindAsync(
            new GetProductInfoByListIdSpec(request.Items.Select(x => x.ProductId).ToList()), cancellationToken);
         var order = new Order()
         {
            CustomerId = contextAccessor.GetUserId(),
            OrderDate = request.OrderDate,
            TotalPrice = 0
         };
         listProductInfo.ForEach(e =>
         {
            order.AddOrderDetail(new OrderDetail()
            {
               OrderId = order.Id,
               ProductInfo = e,
               Quantity = request.Items.FirstOrDefault(c => c.ProductId == e.Id)?.Quantity ?? 1,
            });
         });
         
         await orderRepository.AddAsync(order, cancellationToken);
         
         await orderStartedTopic.Produce(
            new
            {
               OrderId = order.Id, 
               UserId = order.CustomerId,
               OrderCheckoutDetails =
                  order.OrderDetails.Select(e => new { ProductId = e.ProductId, Quantity = e.Quantity })
            }, cancellationToken);
         
         return Results.Ok(ResultModel<Order>.Create(order));
      }
   }

}