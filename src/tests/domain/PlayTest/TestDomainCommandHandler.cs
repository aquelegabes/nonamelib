#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

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
            EventType = TransactionType.Create,
            ModifiedData = args.GetDomain<TestDomain>()
        };

        audits.SaveChanges(audit, TransactionType.Create);
    }

    public TestDomain Handle(TestDomain domain)
    {
        OnBeforeHandle(new AddNewDomainCommandEventArgs(domain));

        testDomains.SaveChanges(domain, TransactionType.Create);

        OnAfterHandle(new AddNewDomainCommandEventArgs(domain));
        return domain;
    }
}
