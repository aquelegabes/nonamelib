namespace NoNameLib.Domain.Extensions;

public static class RepositoryExtensions
{
    public static int SaveChanges<TDomain>(
        this IRepository<TDomain> repository,
        IEnumerable<TDomain> domains,
        TransactionType transactionType)
        where TDomain : class
    {
        int affectedRowCounts = 0;

        foreach (var domain in domains)
        {
            affectedRowCounts += repository.SaveChanges(domain, transactionType);
        }

        return affectedRowCounts;
    }
}

public static class AsyncRepositoryExtensions
{
    public static Task<int> SaveChangesAsync<TDomain>(
        this IAsyncRepository<TDomain> repository,
        IEnumerable<TDomain> domains,
        TransactionType transactionType,
        CancellationToken cancellationToken = default)
        where TDomain : class
    {
        int affectedRows = 0;
        Task<int>[] tasks = Array.Empty<Task<int>>();

        foreach (var domain in domains)
        {
            var result = repository.SaveChangesAsync(domain, transactionType, cancellationToken);

            Array.Fill(tasks, result);
        }

        return new Task<int>(() =>
        {
            var result = Task.WhenAll(tasks).Result;
            affectedRows = result.Sum();
            return affectedRows;
        }, cancellationToken);
    }
}
