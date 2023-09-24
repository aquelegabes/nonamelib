using Dapper;
using NoNameLib.Extensions.Dappper;

namespace NoNameLib.Domain.Tests.SQLite;

public class TestDomainDapperRepository :
    DapperRepository<TestDomain, string>,
    IQueryFiltered<TestDomain, TestDomainFilters>
{
    public TestDomainDapperRepository(DbSession dbSession) : base(dbSession)
    {
    }

    public IQueryable<TestDomain> Get(TestDomainFilters queryFilter)
    {
        const string sqlformat = "SELECT * FROM TestDomain WHERE {0}";
        string conditions = string.Empty;
        Dictionary<string, object> parameters = new();

        if (!string.IsNullOrWhiteSpace(queryFilter.FullName))
            parameters.Add(nameof(queryFilter.FullName), queryFilter.FullName);

        if (!string.IsNullOrWhiteSpace(queryFilter.Id))
            parameters.Add(nameof(queryFilter.Id), queryFilter.Id);

        foreach (var param in parameters)
        {
            if (!string.IsNullOrWhiteSpace(conditions))
                conditions += " AND ";

            conditions = string.Concat(param.Key, "=@", param.Key);
        }

        string sql = string.Format(sqlformat, conditions);
        var cm = new CommandDefinition(sql, parameters);

        return _dbSession.DbConnection.Query<TestDomain>(cm).AsQueryable();
    }
}
