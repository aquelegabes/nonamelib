namespace NoNameLib.Domain.Tests;

public class DomainTestingObjectTests
{
    [Fact]
    public void GetConnectionStringTest_OK()
    {
        var connectionstring = DomainTestingObject.GetConnectionString();

        Assert.NotNull(connectionstring);
    }
}
