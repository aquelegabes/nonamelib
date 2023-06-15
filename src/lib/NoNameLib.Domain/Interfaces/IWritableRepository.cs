namespace NoNameLib.Domain.Interfaces;

public interface IWritableRepository<TDomain, TKey>
    : IUnitOfWork, IDisposable
{
    void Save(
        TDomain domain);

    void Delete(
        TDomain domain);

    void Delete(
        TKey id);

    Task SaveAsync(
        TDomain domain,
        CancellationToken? cancellationToken = null);

    Task DeleteAsync(
        TKey id,
        CancellationToken? cancellationToken = null);

    Task DeleteAsync(
        TDomain domain,
        CancellationToken? cancellationToken = null);
}