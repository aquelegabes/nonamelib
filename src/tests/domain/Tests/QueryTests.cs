namespace NoNameLib.Domain.Tests;

public class QueryTests
{
    [Fact]
    public void PassingTest_QueryGet()
    {
        var testList = new DomainTestingObject();

        var queryHandler = new TestDomainQueryHandler(testList);
        var result = queryHandler.Get();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count() == 4);
    }

    [Fact]
    public void PassingTest_QueryFilterGetName()
    {
        var testList = new DomainTestingObject();

        var queryHandler = new TestDomainQueryHandler(testList);
        var filters = new TestDomainFilters() { FullName = "vinicius" };

        var result = queryHandler.Get(filters);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void PassingTest_QueryFilterGetId()
    {
        var testList = new DomainTestingObject();
        var id = "6909c4c9-5ebc-4d34-960a-b78dc91263e6";

        var queryHandler = new TestDomainQueryHandler(testList);
        var filters = new TestDomainFilters() { Id = id };

        var result = queryHandler.Get(filters);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
