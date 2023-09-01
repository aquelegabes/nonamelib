using NoNameLib.Domain.Handlers;

namespace NoNameLib.Domain.Tests;

public class QueryTest
{
    public static List<TestDomain> GetDomains()
    {
        return new List<TestDomain>()
        {
            new TestDomain("Gabriel Santos", new DateTime(year: 1998, month: 9, day: 4))
            {
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
    }

    [Fact]
    public void PassingTest_QueryGet()
    {
        var domains = GetDomains();
        var queryDomains = new QueryTestDomain(domains);
        var queryHandler = new QueryHandler<TestDomain>(queryDomains);
        var result = queryHandler.Proccess();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count() == 4);
    }
}
