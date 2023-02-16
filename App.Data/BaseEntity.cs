using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Data;

abstract public class BaseEntity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}