using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace RealState.Domain.Entity;
public class BaseEntity
{
    [Key]
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
