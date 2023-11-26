namespace NoNameLib.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int Commit();
    void BeginTransaction();
    void RollbackTransaction();
}

public interface IAsyncUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task RollbackTransactionAsync();
}