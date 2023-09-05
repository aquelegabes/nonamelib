using NoNameLib.Domain.Records;

namespace NoNameLib.Domain.Tests.PlayTest;

public class AddNewDomainCommandEventArgs : CommandEventArgs
{
    public AddNewDomainCommandEventArgs(object domain) : base(domain)
    {
    }
}

public class TestDomainCommandHandler
    : Command, ICommand<TestDomain>
{
    private readonly IRepository<TestDomain> testDomains;
    private readonly IRepository<Auditable<TestDomain>> audits;

    public TestDomainCommandHandler(
        IRepository<TestDomain> testDomains,
        IRepository<Auditable<TestDomain>> audits)
    {
        this.testDomains = testDomains;
        this.audits = audits;

        this.BeforeHandle += AuditBeforeHandle;
    }

    private void AuditBeforeHandle(object sender, CommandEventArgs args)
    {
        Auditable<TestDomain> audit = new()
        {
            AuditDate = DateTime.Now,
            EventType = EventType.Create,
            ModifiedData = args.GetDomain<TestDomain>()
        };

        audits.SaveChanges(audit);
    }

    public TestDomain Handle(TestDomain domain)
    {
        OnBeforeHandle(new AddNewDomainCommandEventArgs(domain));

        testDomains.SaveChanges(domain);

        OnAfterHandle(new AddNewDomainCommandEventArgs(domain));
        return domain;
    }
}
