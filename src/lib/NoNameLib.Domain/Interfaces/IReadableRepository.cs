namespace NoNameLib.Domain.Interfaces;

public interface IReadableRepository<TDomain, TKey>
    : IDisposable
{
    TDomain Get(
        TKey id);

    IQueryable<TDomain> Get(
        Predicate<Func<TDomain, bool>> predicate = null);

    Task<TDomain> GetAsync(
        TKey id);

    Task<IEnumerable<TDomain>> GetAsync(
        Predicate<Func<TDomain, bool>> predicate = null,
        CancellationToken? cancellationToken = null);

    int Count();

    int Count(
        Predicate<Func<TDomain, bool>> predicate = null);

    int CountAsync(
        CancellationToken? cancellationToken = null);

    int CountAsync(
        Predicate<Func<TDomain, bool>> predicate = null,
        CancellationToken? cancellationToken = null);
}