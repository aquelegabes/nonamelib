namespace NoNameLib.Domain.Tests;

public class CommandTests
{
    [Fact]
    public void PassingTest_AddNewDomain()
    {
        var testList = new DomainTestingObject();
        var newDomain = new TestDomain("Alexandre Santos", new DateTime(year: 1998, month: 9, day: 4))
        {
            ContractDate = DateTime.Now,
            BeginDate = DateTime.Now.AddDays(1),
        };
        var domainsCount = testList.TestDomainList.Count;

        var tdCHandler =
            new TestDomainCommandHandler(new TestDomainRepository(testList), new AuditsRepository(testList));

        tdCHandler.Handle(newDomain);

        Assert.True(domainsCount + 1 == testList.TestDomainList.Count);
        Assert.NotEmpty(testList.AuditableList);
    }

    [Fact]
    public void PassingTest_AddNewDomainAndNotify()
    {
        var testList = new DomainTestingObject();
        var newDomain = new TestDomain("Alexandre Santos", new DateTime(year: 1998, month: 9, day: 4))
        {
            ContractDate = DateTime.Now,
            BeginDate = DateTime.Now.AddDays(1),
        };

        var tdCHandler =
            new TestDomainCommandHandler(new TestDomainRepository(testList), new AuditsRepository(testList));

        tdCHandler.AddNotificationsBeforeHandle(new OnAddNewDomainNotification(new NotificationRepository(testList)));

        var domainsCount = testList.TestDomainList.Count;

        tdCHandler.Handle(newDomain);

        Assert.True(domainsCount + 1 == testList.TestDomainList.Count);
        Assert.NotNull(testList.NotificationObjectsList);
        Assert.NotEmpty(testList.AuditableList);
        Assert.NotEmpty(testList.NotificationObjectsList);
        Assert.Single(testList.NotificationObjectsList);
    }
}
