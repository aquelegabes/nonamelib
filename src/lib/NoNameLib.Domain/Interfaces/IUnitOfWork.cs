namespace NoNameLib.Domain.Interfaces;

public interface IUnitOfWork
{
    int Commit();
    Task<int> CommitAsync(CancellationToken? cancellationToken = null);
}