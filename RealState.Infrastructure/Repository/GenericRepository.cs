using System;
using System.Linq.Expressions;
using MongoDB.Driver;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;

namespace RealState.Infrastructure.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public GenericRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }
    
    public void Add(T entity)
    {
        _collection.InsertOne(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _collection.InsertMany(entities);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return await _collection.Find(expression).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    
    

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _collection.Find(expression).ToList();
    }

    public async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }


    public void Remove(T entity)
    {
        _collection.DeleteOne(x => x.Id == entity.Id);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        var ids = entities.Select(e => e.Id);
        _collection.DeleteMany(x => ids.Contains(x.Id));
    }

    public void Update(T entity)
    {
        _collection.ReplaceOne(x => x.Id == entity.Id, entity);
    }
    
    public async Task<T> AddAndReturnAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}