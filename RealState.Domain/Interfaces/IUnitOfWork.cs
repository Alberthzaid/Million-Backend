namespace RealState.Domain.Interfaces;

public interface IUnitOfWork
{
    IProperty Property { get; }
    IOwner Owner { get; }
    IImageProperty ImageProperty { get; }
}