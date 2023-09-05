namespace NoNameLib.Domain.Tests.PlayTest;

public class AuditsRepository : IRepository<Auditable<TestDomain>>
{
    private readonly List<Auditable<TestDomain>> _auditable;
    public AuditsRepository(
        TestList testList)
    {
        _auditable = testList.AuditableList;
    }

    public void Delete(Auditable<TestDomain> domain)
    {
        _auditable.Remove(domain);
    }

    public int SaveChanges(Auditable<TestDomain> domain)
    {
        ValidationHandler.Validate(domain);
        _auditable.Add(domain);
        return 1;
    }
}
