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

public interface IQueryFiltered<TDomain>
    where TDomain : class
{
    IQueryable<TDomain> Get(
        QueryFilter queryFilter);
}

public interface IQueryFilteredAsync<TDomain> : IDisposable
    where TDomain : class
{
    Task<IQueryable<TDomain>> GetAsync(
        QueryFilter queryFilter,
        CancellationToken cancellationToken = default);
}