using NoNameLib.Domain.Records;

namespace NoNameLib.Domain.Tests.PlayTest;

public class AddNewDomainCommandEventArgs : CommandEventArgs
{
    public AddNewDomainCommandEventArgs(object domain) : base(domain)
    {
    }
}

public class AddNewDomainCommand
    : Command, ICommand<TestDomain>
{
    private readonly List<TestDomain> testDomains;
    private readonly List<Auditable<TestDomain>> audits;

    public AddNewDomainCommand(
        List<TestDomain> testDomains,
        List<Auditable<TestDomain>> audits)
    {
        this.testDomains = testDomains;
        this.audits = audits;

        this.CommandSent += AuditOnSend;
    }

    private void AuditOnSend(object sender, CommandEventArgs args)
    {
        Auditable<TestDomain> audit = new()
        {
            AuditDate = DateTime.Now,
            EventType = EventType.Create,
            ModifiedData = args.GetDomain<TestDomain>()
        };

        audits.Add(audit);
    }

    public TestDomain Send(TestDomain domain)
    {
        OnCommandSent(new AddNewDomainCommandEventArgs(domain));
        testDomains.Add(domain);
        return domain;
    }
}
