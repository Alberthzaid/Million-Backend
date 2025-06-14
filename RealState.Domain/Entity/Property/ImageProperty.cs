using MongoDB.Bson.Serialization.Attributes;

namespace RealState.Domain.Entity;

public class ImageProperty : BaseEntity
{
    [BsonElement("idProperty")]
    public String idProperty { get; set; }
    
    [BsonElement("fileLink")]
    public String fileLink { get; set; }
}