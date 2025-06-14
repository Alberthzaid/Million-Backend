using MongoDB.Driver;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;

namespace RealState.Infrastructure.Repository;

public class ImagePropertyRepository : GenericRepository<ImageProperty> , IImageProperty 
{
    private readonly IMongoCollection<ImageProperty> _collection;

    public ImagePropertyRepository(MongoDbContext context) : base(context.ImageProperties)
    {
        _collection = context.ImageProperties;
    }
}