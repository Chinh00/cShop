using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cShop.Core.Domain;
[Index(nameof(Id))]
public class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = TimeZoneInfo
        .ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
    public DateTime? UpdatedDate { get; set; } 
}

public class OutboxEntity : EntityBase
{
    public string AggregateType { get; set; }
    public string AggregateId { get; set; }
    public string Type { get; set; }
    public byte[] Payload { get; set; }
}

public class ProjectionBase
{
    [BsonId]
    public Guid Id { get; set; }
    public long Version { get; set; }
}

public class MongoEntityBase
{
    [BsonId] public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}