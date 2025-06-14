using MongoDB.Driver;
using RealState.Domain;
using RealState.Domain.Entity;
using RealState.Domain.Entity.Owner;

namespace RealState.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase("Millions"); 
    }

    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("property");
    public IMongoCollection<ImageProperty> ImageProperties => _database.GetCollection<ImageProperty>("imageProperty");
    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owner");
}
