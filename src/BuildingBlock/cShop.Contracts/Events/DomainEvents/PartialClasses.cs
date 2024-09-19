using cShop.Core.Domain;
using MediatR;

namespace cShop.Contracts.Events.DomainEvents;







public record ProductCreatedDomainEvent(
    Guid ProductId,
    string Name,
    float CurrentCost,
    string ImageSrc,
    Guid? CategoryId,
    int Version) : Message, IDomainEvent, INotification
{
    
}


public record ProductNameUpdatedDomainEvent(Guid ProductId, string Name, int Version) : Message, IDomainEvent, INotification
{
}