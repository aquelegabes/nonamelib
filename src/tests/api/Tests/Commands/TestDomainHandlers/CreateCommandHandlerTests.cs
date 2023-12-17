namespace NoNameLib.Api.Tests.Commands.TestDomainHandlers;

public class CreateCommandHandlerTests
{
    [Fact]
    public void HandleCreate_OK()
    {
        var testList = new DomainTestingObject();
        var newModel = new TestModel()
        {
            FullName = "Alexandre Santos",
            CPF = "47744020871",
            ContractDate = DateTime.Now,
            BeginDate = DateTime.Now.AddDays(1),
            BirthDate = new DateTime(year: 1998, month: 9, day: 4)
        };

        var domainsCount = testList.TestDomainList.Count;

        var mapper = MockObjects.GetMapper();
        var uowMock = MockObjects.GetUnitOfWorkMock();

        var tdCHandler =
            new TestDomainCreateCommandHandler(
                new TestDomainRepository(testList),
                uowMock.Object,
                mapper);

        tdCHandler.Handle(newModel);

        Assert.True(domainsCount + 1 == testList.TestDomainList.Count);

        uowMock.Verify(
            uow => uow.BeginTransaction(),
            Times.Once);
        uowMock.Verify(
            uow => uow.Commit(),
            Times.Once);
    }
}
