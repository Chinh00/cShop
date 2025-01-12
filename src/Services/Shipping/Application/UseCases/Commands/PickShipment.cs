using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.IdentityServer;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record PickShipment(Guid OrderId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<PickShipment>
    {
        public Validator()
        {
            
        }      
    }
    internal class Handler(
        IRepository<ShipperOrder> repository,
        ITopicProducer<ShipmentPickedIntegrationEvent> shipmentPicked,
        IClaimContextAccessor contextAccessor) : IRequestHandler<PickShipment, IResult>
    {
        
        public async Task<IResult> Handle(PickShipment request, CancellationToken cancellationToken)
        {
            var shipment = await repository.FindByIdAsync(request.OrderId, cancellationToken);
            shipment.ShipperId = contextAccessor.GetUserId();
            await repository.UpdateAsync(shipment, cancellationToken);
            await shipmentPicked.Produce(new { request.OrderId }, cancellationToken);
            return Results.Ok(ResultModel<ShipperOrder>.Create(shipment));
        }
    }
}