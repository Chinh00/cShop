using System.Linq.Expressions;
using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace EventStore;

public class StoreEventConverter() : ValueConverter<IDomainEvent?, string>(
    @event => JsonConvert.SerializeObject(@event, typeof(IDomainEvent),
        new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }),
    jsonString => JsonConvert.DeserializeObject<IDomainEvent>(jsonString,
        new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }));