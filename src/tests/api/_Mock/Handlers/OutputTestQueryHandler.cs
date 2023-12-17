namespace NoNameLib.Api.Tests.Mock.Handlers;

public class OutputTestQueryHandler :
    IQuery<OutputTest>,
    IAsyncQuery<OutputTest>,
    IQueryFiltered<OutputTest, OutputTestQueryFilter>,
    IAsyncQueryFiltered<OutputTest, OutputTestQueryFilter>
{
    private static List<OutputTest> Outputs()
    {
        var list = new List<OutputTest>();

        for (int i = 0; i < 10; i++)
        {
            list.Add(new OutputTest() { Name = $"Register #{i}" });
        }

        return list;
    }

    public IQueryable<OutputTest> Get()
    {
        return Outputs().AsQueryable();
    }

    public IQueryable<OutputTest> Get(OutputTestQueryFilter queryFilter)
    {
        var querybase = Outputs().AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryFilter.Name))
            querybase = querybase.Where(_ => queryFilter.Name == _.Name);

        return querybase;
    }

    public async Task<IQueryable<OutputTest>> Get(CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Get();
    }

    public async Task<IQueryable<OutputTest>> Get(
        OutputTestQueryFilter queryFilter, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Get(queryFilter);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
