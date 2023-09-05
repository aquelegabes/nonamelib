namespace NoNameLib.Domain.Interfaces;

public interface IQuery<TDomain>
    where TDomain : class
{
    IQueryable<TDomain> Get();
}

public interface IQueryAsync<TDomain> : IDisposable
    where TDomain : class
{
    Task<IQueryable<TDomain>> GetAsync(
        CancellationToken cancellationToken = default);
}

public interface IQueryFiltered<TDomain, TFilter>
    where TDomain : class
    where TFilter : QueryFilter
{
    IQueryable<TDomain> Get(
        TFilter queryFilter);
}

public interface IQueryFilteredAsync<TDomain, TFilter> : IDisposable
    where TDomain : class
    where TFilter : QueryFilter
{
    Task<IQueryable<TDomain>> GetAsync(
        TFilter queryFilter,
        CancellationToken cancellationToken = default);
}