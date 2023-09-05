namespace NoNameLib.Domain.Interfaces;

public interface IUnitOfWork
{
    IRepository<TDomain> GetRepository<TDomain>()
        where TDomain : class;
    int Commit();
    Task<int> CommitAsync();
}
