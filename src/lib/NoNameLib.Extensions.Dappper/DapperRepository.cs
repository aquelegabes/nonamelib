#pragma warning disable CS8604 // Possible null reference argument.
using System.Reflection;
using System.Runtime.Serialization;
using NoNameLib.Domain.Validation;

namespace NoNameLib.Extensions.Dappper;

public abstract class DapperRepository<TDomain, TKey> :
    IRepository<TDomain>,
    IQuery<TDomain>
    where TDomain : class, IDomain<TKey>
    where TKey : class
{
    protected bool disposing = false;
    private bool disposedValue;

    protected readonly DbSession _dbSession;

    protected string[] DomainPropertyNames =>
        DomainPropertyInfosOrderedByName.Select(prop => prop.Name).ToArray();
    protected IOrderedEnumerable<PropertyInfo>? DomainPropertyInfosOrderedByName =>
        typeof(TDomain).GetProperties().OrderBy(prop => prop.Name);


    protected DapperRepository(
        DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _dbSession.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual int Create(TDomain domain)
    {
        ValidationHandler.Validate(domain);
        Dictionary<string, object> parameters = new();
        List<string> fieldNames = new();

        foreach (string propertyName in DomainPropertyNames)
        {
            var key = propertyName;
            var propInfo = DomainPropertyInfosOrderedByName
                    .Single(prop =>
                        prop.Name == propertyName
                        && !prop.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), true).Any());

            var value = propInfo.GetValue(domain);

            if (value == null) { continue; }

            parameters.Add(key, value);
            fieldNames.Add(key);
        }

        var sql =
@$"INSERT INTO {typeof(TDomain).Name} ({string.Join(',', fieldNames)})
VALUES ({string.Join(',', fieldNames.Select(_ => "@" + _))})";

        var cm = new CommandDefinition(sql, parameters, _dbSession.Transaction);
        return _dbSession.DbConnection.Execute(cm);
    }

    protected virtual int Update(TDomain domain)
    {
        ValidationHandler.Validate(domain);
        Dictionary<string, object> parameters = new();
        List<string> fieldNames = new();

        parameters.Add("Id", domain.Id.ToString());
        foreach (string propertyName in DomainPropertyNames)
        {
            var key = propertyName;
            var propInfo = DomainPropertyInfosOrderedByName
                    .FirstOrDefault(prop => 
                        prop.Name == propertyName
                        && prop.GetCustomAttributes(typeof(MutableDataMemberAttribute), true).Any());

            if (propInfo is null) continue;

            var value = propInfo.GetValue(domain);

            if (value == null) continue;

            parameters.Add(key, value);
            fieldNames.Add(key);
        }

        var sql =
$@"UPDATE {typeof(TDomain).Name}
    SET 
        {string.Join(
                ',',
                fieldNames.Select(fieldName => string.Concat(fieldName, " = @", fieldName)))}
WHERE Id = @Id";

        var cm = new CommandDefinition(sql, parameters, _dbSession.Transaction);
        return _dbSession.DbConnection.Execute(cm);
    }

    public int SaveChanges(TDomain domain, TransactionType eventType)
    {
        var affectedRows = eventType switch
        {
            TransactionType.Update => Update(domain),
            TransactionType.Create => Create(domain),
            TransactionType.Delete => Delete(domain),
            _ => 0,
        };

        _dbSession.LastTransactionAffectedRows += affectedRows;
        return affectedRows;
    }

    protected virtual int Delete(TDomain domain)
    {
        string sql = $"DELETE FROM {nameof(TDomain)} WHERE Id = @Id";
        object parameters = new { Id = domain.Id.ToString() };

        var cm = new CommandDefinition(sql, parameters, _dbSession.Transaction);
        return _dbSession.DbConnection.Execute(cm);
    }

    protected virtual IQueryable<TDomain> GetAll()
    {
        string sql =
@$"SELECT * FROM {typeof(TDomain).Name}";

        return _dbSession.DbConnection.Query<TDomain>(sql).AsQueryable();
    }

    public IQueryable<TDomain> Get() => GetAll();
}