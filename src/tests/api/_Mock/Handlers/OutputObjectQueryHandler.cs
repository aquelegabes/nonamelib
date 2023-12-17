namespace NoNameLib.Application.Tests.Mock.Handlers;

public class OutputObjectQueryHandler :
    IQuery<OutputObject>,
    IAsyncQuery<OutputObject>,
    IQueryFiltered<OutputObject, OutputObjectQueryFilter>,
    IAsyncQueryFiltered<OutputObject, OutputObjectQueryFilter>
{
    private static List<OutputObject> Outputs()
    {
        var list = new List<OutputObject>();

        for (int i = 0; i < 10; i++)
        {
            list.Add(new OutputObject() { Name = $"Register #{i}" });
        }

        return list;
    }

    public IQueryable<OutputObject> Get()
    {
        return Outputs().AsQueryable();
    }

    public IQueryable<OutputObject> Get(OutputObjectQueryFilter queryFilter)
    {
        var querybase = Outputs().AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryFilter.Name))
            querybase = querybase.Where(_ => queryFilter.Name == _.Name);

        return querybase;
    }

    public async Task<IQueryable<OutputObject>> Get(CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Get();
    }

    public async Task<IQueryable<OutputObject>> Get(
        OutputObjectQueryFilter queryFilter, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Get(queryFilter);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
