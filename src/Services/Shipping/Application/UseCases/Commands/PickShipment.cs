using cShop.Core.Domain;
using cShop.Core.Repository;
using Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record PickShipment(Guid OrderId, Guid ShipperId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<PickShipment>
    {
        public Validator()
        {
            
        }      
    }
    internal class Handler(IRepository<ShipperOrder> repository, ITopicProducer<ShipmentPickedIntegrationEvent> shipmentPicked) : IRequestHandler<PickShipment, IResult>
    {
        
        public async Task<IResult> Handle(PickShipment request, CancellationToken cancellationToken)
        {
            var shipment = await repository.FindByIdAsync(request.OrderId, cancellationToken);
            shipment.ShipperId = request.ShipperId;
            await repository.UpdateAsync(shipment, cancellationToken);
            await shipmentPicked.Produce(new { request.OrderId }, cancellationToken);
            return Results.Ok(ResultModel<ShipperOrder>.Create(shipment));
        }
    }
}