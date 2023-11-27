namespace NoNameLib.Domain.Tests.Tests;

public class RandomTests
{
    [Fact]
    public void GetConnectionStringTest_OK()
    {
        var connectionstring = DomainTestingObject.GetConnectionString();

        Assert.NotNull(connectionstring);
    }
}
