using NoNameLib.Api.Tests.PlayTest;
using NoNameLib.Domain.Tests.PlayTest;

namespace NoNameLib.Api.Tests.Commands;

public class CreateCommandHandlerTests
{
    [Fact]
    public void HandleCreate_OK()
    {
        var testList = new DomainTestingObject();
        var newDomain = new TestDomain("Alexandre Santos", new DateTime(year: 1998, month: 9, day: 4))
        {
            ContractDate = DateTime.Now,
            BeginDate = DateTime.Now.AddDays(1),
        };
        var domainsCount = testList.TestDomainList.Count;


        //var tdCHandler =
        //    new TestDomainCreateCommandHandler(
        //        new TestDomainRepository(testList),
        //        );


        //var result = tdCHandler.Handle(newDomain);
        //Assert.NotNull(result);
        Assert.True(domainsCount + 1 == testList.TestDomainList.Count);
        Assert.NotEmpty(testList.AuditableList);
    }
}
