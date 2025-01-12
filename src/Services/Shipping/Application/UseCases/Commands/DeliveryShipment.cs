using cShop.Core.Domain;
using FluentValidation;
using IntegrationEvents;
using MassTransit;
using MediatR;

namespace Application.UseCases.Commands;

public record DeliveryShipment(Guid OrderId) : ICommand<IResult>
{
    public class Validator : AbstractValidator<DeliveryShipment>
    {
        public Validator()
        {
            
        }
    }
    
    internal class Handler(ITopicProducer<ShipmentDeliveryIntegrationEvent> shipmentDeliveryIntegrationEvent)
        : IRequestHandler<DeliveryShipment, IResult>
    {

        public async Task<IResult> Handle(DeliveryShipment request, CancellationToken cancellationToken)
        {
            await shipmentDeliveryIntegrationEvent.Produce(new {request.OrderId}, cancellationToken);
            return Results.Ok();
        }
    }
    
    
}