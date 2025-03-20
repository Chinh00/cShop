using cShop.Core.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class CommentLine : MongoEntityBase
{
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    public UserInfo User { get; set; }
    [BsonRepresentation(BsonType.String)]

    public Guid ProductId { get; set; }
    public string Content { get; set; }
    public List<CommentLine> CommentLines { get; set; }
}