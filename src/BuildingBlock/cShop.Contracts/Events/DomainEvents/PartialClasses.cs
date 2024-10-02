using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Events.DomainEvents;







public record ProductCreatedDomainEvent(
    Guid ProductId,
    string Name,
    float CurrentCost,
    string ImageSrc,
    Guid? CategoryId,
    long Version) : Message, IDomainEvent, INotification
{
    
}


public record ProductNameUpdatedDomainEvent(Guid ProductId, string Name, long Version) : Message, IDomainEvent, INotification
{
}