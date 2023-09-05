namespace NoNameLib.Domain.Interfaces;

public interface IRepository<TDomain>
    where TDomain : class
{
    int SaveChanges(TDomain domain);
    void Delete(TDomain domain);
}

public interface IAsyncRepository<TDomain>
    where TDomain : class
{
    Task<int> SaveChangesAsync(TDomain domain);
    Task DeleteAsync(TDomain domain);
}