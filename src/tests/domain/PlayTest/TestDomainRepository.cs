namespace NoNameLib.Domain.Tests.PlayTest;

public class TestDomainRepository : IRepository<TestDomain>
{
    private readonly List<TestDomain> _domains;

    public TestDomainRepository(
        TestList testList)
    {
        _domains = testList.TestDomainList;
    }

    public void Delete(TestDomain domain)
    {
        _domains.Remove(domain);
    }

    public int SaveChanges(TestDomain domain)
    {
        ValidationHandler.Validate(domain);
        _domains.Add(domain);
        return 1;
    }
}
