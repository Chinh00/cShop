using Confluent.SchemaRegistry;
using cShop.Core.Domain;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain;
using Domain.Outboxs;
using FluentValidation;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateShipperCommand(
    string Name, string Phone) : ICommand<IResult>
{
    public class Validator : AbstractValidator<CreateShipperCommand>
    {
        public Validator()
        {
            
        }
    }
    internal class Handler(ISchemaRegistryClient schemaRegistryClient, IRepository<Shipper> repository, IRepository<ShipperOutbox> outboxRepository) : OutboxHandler<ShipperOutbox>(schemaRegistryClient, outboxRepository), IRequestHandler<CreateShipperCommand, IResult>
    {
        public async Task<IResult> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
        {
            var shipper = new Shipper()
            {
                Name = request.Name,
                Phone = request.Phone
            };
            await repository.AddAsync(shipper, cancellationToken);
            await SendToOutboxAsync(shipper, () => (
                new ShipperOutbox(),
                new ShipperCreatedIntegrationEvent() { Id = shipper.Id.ToString(), Name = shipper.Name },
                "shipper_cdc_events"), cancellationToken);
            return Results.Ok(ResultModel<Shipper>.Create(shipper));
        }
    }
    
    
}