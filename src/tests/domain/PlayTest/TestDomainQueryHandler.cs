namespace NoNameLib.Domain.Tests.PlayTest;

public class TestDomainFilters : QueryFilter
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class TestDomainQueryHandler :
    IQuery<TestDomain>,
    IQueryFiltered<TestDomain, TestDomainFilters>
{
    private readonly List<TestDomain> _domains;

    public TestDomainQueryHandler(TestList statics)
    {
        _domains = statics.TestDomainList;
    }

    public IQueryable<TestDomain> Get() => _domains.AsQueryable();

    public IQueryable<TestDomain> Get(TestDomainFilters queryFilter)
    {
        var queryBase = _domains.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryFilter.Id))
            queryBase = queryBase.Where(_ => _.Id.Equals(queryFilter.Id));

        if (!string.IsNullOrWhiteSpace(queryFilter.Name))
            queryBase = queryBase.Where(_ => _.FullName.Contains(queryFilter.Name, StringComparison.InvariantCultureIgnoreCase));

        return queryBase;
    }
}
