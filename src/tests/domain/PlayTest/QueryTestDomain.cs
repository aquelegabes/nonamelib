namespace NoNameLib.Domain.Tests.PlayTest;

public class QueryTestDomainFilters : QueryFilter
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class QueryTestDomain : IQuery<TestDomain>
{
    private readonly List<TestDomain> _domains;

    public QueryTestDomain(List<TestDomain> domains)
    {
        this._domains = domains;
    }

    public IQueryable<TestDomain> Get()
    {
        return _domains.AsQueryable();
    }

    public async Task<IQueryable<TestDomain>> GetAsync(
        CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => Get(), cancellationToken);
    }

}
