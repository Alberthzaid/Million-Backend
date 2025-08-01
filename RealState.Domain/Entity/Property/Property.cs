using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealState.Domain.Entity;

public class Property : BaseEntity
{

    [BsonElement("idOwner")]
    public string IdOwner { get; set; } = string.Empty;
        
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
        
    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;
        
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}