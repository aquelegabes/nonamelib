namespace NoNameLib.Domain.Interfaces;

public interface IRepository<TDomain> : IDisposable
    where TDomain : class
{
    int SaveChanges(
        TDomain domain,
        TransactionType transactionType);
}

public interface IAsyncRepository<TDomain> : IDisposable
    where TDomain : class
{
    Task<int> SaveChangesAsync(
        TDomain domain,
        TransactionType transactionType,
        CancellationToken cancellationToken = default);
}