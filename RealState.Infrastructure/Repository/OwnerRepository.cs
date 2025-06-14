using MongoDB.Driver;
using RealState.Domain.Entity.Owner;
using RealState.Domain.Interfaces;

namespace RealState.Infrastructure.Repository;

public class OwnerRepository : GenericRepository<Owner> , IOwner
{
    private readonly IMongoCollection<Owner> _collection;
    
    public OwnerRepository(MongoDbContext context) : base(context.Owners)
    {
        _collection = context.Owners;
    }    
}