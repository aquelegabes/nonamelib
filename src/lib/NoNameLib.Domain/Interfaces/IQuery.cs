namespace NoNameLib.Domain.Interfaces;

public interface IQuery<TDomain> : IBaseQuery
    where TDomain : class
{
    IQueryable<TDomain> Get();
}

public interface IAsyncQuery<TDomain> : IBaseQuery, IDisposable
    where TDomain : class
{
    Task<IQueryable<TDomain>> Get(
        CancellationToken cancellationToken = default);
}

public interface IQueryFiltered<TDomain, TFilter> : IBaseQuery
    where TDomain : class
    where TFilter : QueryFilter
{
    IQueryable<TDomain> Get(
        TFilter queryFilter);
}

public interface IAsyncQueryFiltered<TDomain, TFilter> : IBaseQuery, IDisposable
    where TDomain : class
    where TFilter : QueryFilter
{
    Task<IQueryable<TDomain>> Get(
        TFilter queryFilter,
        CancellationToken cancellationToken = default);
}

public interface IBaseQuery { }