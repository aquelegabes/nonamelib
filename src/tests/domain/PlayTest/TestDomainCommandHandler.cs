#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

using NoNameLib.Domain.Abstract;
using NoNameLib.Domain.Enums;

namespace NoNameLib.Domain.Tests.PlayTest;

internal class AddNewDomainCommandEventArgs : NotifiableEventArgs
{
    public AddNewDomainCommandEventArgs(object domain) : base(domain)
    {
    }
}

internal class TestDomainCommandHandler :
    Notifiable,
    ICommand<TestDomain>
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

    private void AuditBeforeHandle(object sender, NotifiableEventArgs args)
    {
        Auditable<TestDomain> audit = new()
        {
            AuditDate = DateTime.Now,
            EventType = TransactionType.Create,
            ModifiedData = args.GetData<TestDomain>()
        };

        audits.SaveChanges(audit, TransactionType.Create);
    }

    public TestDomain Handle(TestDomain domain)
    {
        OnBeforeHandle(new AddNewDomainCommandEventArgs(domain));

        testDomains.SaveChanges(domain, TransactionType.Create);

        return domain;
    }
}
