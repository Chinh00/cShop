﻿
using System.Text.Json.Serialization;
using MediatR;

namespace cShop.Core.Domain;


public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}

public interface IEvent
{
    
}

public interface IIntegrationEvent : IEvent, INotification {}

public interface IVersion
{
    long Version { get; }
}

public interface IDomainEvent : IEvent, IVersion, IMessage
{
}


public interface IMessage
{
    DateTime CreateAt { get; }
}



public abstract record Message : IMessage, INotification
{
    public DateTime CreateAt { get; } = DateTime.UtcNow;
}


public abstract class AggregateBase : EntityBase, IAggregateRoot
{
    private ICollection<IDomainEvent> _events;

    public long Version { get; set; }

    public AggregateBase()
    {
        _events = new List<IDomainEvent>();
        Version = 0;
    }

    public void RaiseEvent(Func<long, IDomainEvent> func)
    {
        _events ??= [];
        Version++;
        var @event = func(Version);
        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events ??= [];
        Version = 0;
        _events.Clear();
    }

    public void ApplyEvents(ICollection<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            this.ApplyEvent(domainEvent);
            Version = domainEvent.Version;
        }
    }
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.ToList().AsReadOnly();

    public abstract void ApplyEvent(IDomainEvent @event);

}



public class DomainException(string message) : Exception(message);