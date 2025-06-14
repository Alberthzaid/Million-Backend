using RealState.Domain.Interfaces;
using RealState.Infrastructure;
using RealState.Infrastructure.Repository;

namespace RealState.Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork , IDisposable
{
    private readonly MongoDbContext _context;
    private IProperty? _property;
    private IImageProperty? _imageProperty;
    private IOwner? _owner;
    
    public UnitOfWork(MongoDbContext context)
    {
        _context = context;
    }

    public IProperty Property
    {
        get
        {
            _property ??= new PropertyRepository(_context);
            return _property;
        }
    }

    public IOwner Owner
    {
        get
        {
            _owner ??= new OwnerRepository(_context);
            return _owner;
        }
    }

    public IImageProperty ImageProperty
    {
        get
        {
            _imageProperty ??= new ImagePropertyRepository(_context);
            return _imageProperty;
        }
    }

    public void Dispose() {}
}