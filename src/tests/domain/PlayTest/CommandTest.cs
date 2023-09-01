using NoNameLib.Domain.Handlers;

namespace NoNameLib.Domain.Tests.PlayTest;

public class CommandTest
{
    public List<TestDomain> GetDomains() => QueryTest.GetDomains();

    [Fact]
    public void PassingTest_AddNewDomain()
    {
        var domains = GetDomains();
        var newDomain = new TestDomain("Alexandre Santos", new DateTime(year: 1998, month: 9, day: 4))
        {
            ContractDate = DateTime.Now,
            BeginDate = DateTime.Now.AddDays(1),
        };

        var audits = new List<Records.Auditable<TestDomain>>();
        var addNewDomainCommand = new AddNewDomainCommand(domains, audits);
        var commandHandler = new CommandHandler<TestDomain>(addNewDomainCommand);
        var domainsCount = domains.Count;

        var result = commandHandler.Send(newDomain);
        Assert.NotNull(result);
        Assert.True(domainsCount + 1 == domains.Count);
        Assert.NotEmpty(audits);
    }
}
