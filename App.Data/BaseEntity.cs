using App.Data.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Data;

public abstract class BaseEntity : IEntity
{
    protected BaseEntity()
    {
        this.CreatedTs = DateTime.UtcNow;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [IndexedProperty("shard")]
    public string? Id { get; set; }

    public DateTime CreatedTs { get; set; }

    public DateTime? UpdatedTs { get; set; } = null;
}