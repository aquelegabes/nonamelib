global using Xunit;
global using NoNameLib.Domain.Interfaces;
global using NoNameLib.Domain.Utils;
global using NoNameLib.Domain.Validation;
global using NoNameLib.Domain.Tests.PlayTest;
global using NoNameLib.Domain.Records;

public class TestList
{
    public List<TestDomain> TestDomainList;
    public List<Auditable<TestDomain>> AuditableList;
    public List<NotificationObject> NotificationObjectsList;

    public TestList()
    {
        TestDomainList = new()
            {
                new TestDomain("Gabriel Santos", new DateTime(year: 1998, month: 9, day: 4))
                {
                    Id = "6909c4c9-5ebc-4d34-960a-b78dc91263e6",
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
                new TestDomain("Alex Mujica", new DateTime(year: 1992, month: 5, day: 13))
                {
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
                new TestDomain("Teodoro Santos", new DateTime(year: 1977, month: 1, day: 22))
                {
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
                new TestDomain("Vinicius Fonseca", new DateTime(year: 2004, month: 12, day: 13))
                {
                    ContractDate = DateTime.Now,
                    BeginDate = DateTime.Now.AddDays(1),
                },
            };
        AuditableList = new();
        NotificationObjectsList = new();
    }
}