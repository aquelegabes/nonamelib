using NoNameLib.Domain.Enums;
using NoNameLib.Domain.Extensions;

namespace NoNameLib.Domain.Tests.PlayTest;

public class TestDomainRepository : IRepository<TestDomain>
{
    private readonly List<TestDomain> _domains;

    public TestDomainRepository(
        DomainTestingObject testList)
    {
        _domains = testList.TestDomainList;
    }

    public void Delete(TestDomain domain)
    {
        _domains.Remove(domain);
    }

    public void Dispose()
    {
    }

    public int SaveChanges(TestDomain domain, TransactionType eventType)
    {
        domain.Validate();
        _domains.Add(domain);
        return 1;
    }
}
