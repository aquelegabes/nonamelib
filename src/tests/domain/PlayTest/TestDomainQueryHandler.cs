namespace NoNameLib.Domain.Tests.PlayTest;

internal class TestDomainQueryHandler :
    IQuery<TestDomain>,
    IQueryFiltered<TestDomain, TestDomainFilters>
{
    private readonly List<TestDomain> _domains;

    public TestDomainQueryHandler(DomainTestingObject statics)
    {
        _domains = statics.TestDomainList;
    }

    public IQueryable<TestDomain> Get() => _domains.AsQueryable();

    public IQueryable<TestDomain> Get(TestDomainFilters queryFilter)
    {
        var queryBase = _domains.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryFilter.Id))
            queryBase = queryBase.Where(_ => _.Id.Equals(queryFilter.Id));

        if (!string.IsNullOrWhiteSpace(queryFilter.FullName))
            queryBase = queryBase.Where(_ => _.FullName.Contains(queryFilter.FullName, StringComparison.InvariantCultureIgnoreCase));

        return queryBase;
    }
}
