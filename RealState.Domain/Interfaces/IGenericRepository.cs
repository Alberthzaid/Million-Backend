using System.Linq.Expressions;
using MongoDB.Driver;
using RealState.Domain.Entity;

namespace RealState.Domain.Interfaces;

public interface IGenericRepository <T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add (T entity );
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter);
    Task AddAsync(T entity);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> AddAndReturnAsync(T entity);
}