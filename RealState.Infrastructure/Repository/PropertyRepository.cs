using MongoDB.Driver;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;

namespace RealState.Infrastructure.Repository;

public class PropertyRepository : GenericRepository<Property> , IProperty
{
    private readonly IMongoCollection<Property> _properties;

    public PropertyRepository(MongoDbContext context) : base(context.Properties)
    {
        _properties = context.Properties;
    }
    
    
}