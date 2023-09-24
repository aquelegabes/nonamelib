using NoNameLib.Domain.Tests.SQLite;

namespace NoNameLib.Domain.Tests.Tests;

public class TestDomainDapperQueryHandler :
    IQuery<TestDomain>,
    IQueryFiltered<TestDomain, TestDomainFilters>
{
    private readonly TestDomainDapperRepository repo;

    public TestDomainDapperQueryHandler(
        TestDomainDapperRepository repo)
    {
        this.repo = repo;
    }

    public IQueryable<TestDomain> Get() => repo.Get();

    public IQueryable<TestDomain> Get(TestDomainFilters queryFilter) => repo.Get(queryFilter);
}