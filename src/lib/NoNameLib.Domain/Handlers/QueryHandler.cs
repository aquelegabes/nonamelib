namespace NoNameLib.Domain.Handlers;

public class QueryHandler<TDomain>
    where TDomain : class
{
    private readonly IQuery<TDomain> query;
    private readonly IQueryFiltered<TDomain> queryFiltered;

    public QueryHandler(
        IQuery<TDomain> query)
    {
        this.query = query;
    }

    public QueryHandler(
        IQueryFiltered<TDomain> queryFiltered)
    {
        this.queryFiltered = queryFiltered;
    }

    public IEnumerable<TDomain> Proccess(
        QueryFilter filter = null)
    {
        return filter is null && queryFiltered is null ?
            query.Get()
            : queryFiltered.Get(filter);
    }
}