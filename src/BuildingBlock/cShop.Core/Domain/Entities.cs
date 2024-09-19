using MongoDB.Bson.Serialization.Attributes;

namespace cShop.Core.Domain;

public class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; } 
}

public class ProjectionBase
{
    [BsonId]
    public Guid Id { get; set; }
    public long Version { get; set; }
}