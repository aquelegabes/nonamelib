namespace NoNameLib.Domain.Interfaces;

public interface IRepository<TDomain, TKey>
    : IWritableRepository<TDomain, TKey>, IReadableRepository<TDomain, TKey>
{

}