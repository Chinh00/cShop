using Confluent.SchemaRegistry;
using cShop.Core.Repository;
using cShop.Infrastructure.SchemaRegistry;
using Domain.Outbox;
using IntegrationEvents;
using MediatR;

namespace Application.UseCases.Masstransits;

public sealed class PaymentPrepareConsumer(
    ISchemaRegistryClient schemaRegistryClient,
    IRepository<OrderOutbox> repository) : OutboxHandler<OrderOutbox>(schemaRegistryClient, repository),
    INotificationHandler<PaymentPrepareIntegrationEvent> 
{
    public async Task Handle(PaymentPrepareIntegrationEvent notification, CancellationToken cancellationToken)
    {
        // send to order outbox
        
    }
}