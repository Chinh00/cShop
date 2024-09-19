
using MediatR;
using Newtonsoft.Json;

namespace cShop.Core.Domain;


public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}

public interface IEvent
{
    
}


public interface IVersion
{
    int Version { get; }
}

public interface IDomainEvent : IEvent, IVersion, IMessage
{
}

public interface IMessage
{
    DateTime CreateAt { get; }
}



public abstract record Message : IMessage
{
    public DateTime CreateAt { get; }
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
public interface ITxRequest {}


