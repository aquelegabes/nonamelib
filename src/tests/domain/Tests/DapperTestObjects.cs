using System.Data;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using NoNameLib.Extensions.Dappper;

namespace NoNameLib.Domain.Tests;

public partial class DapperRepositoryTests
{
    public class DapperTestObjects : IDisposable
    {
        public DbSession DbSession { get; }
        public IDbConnection DbConnection { get; }
        public IUnitOfWork UnityOfWork { get; }
        public IRepository<TestDomain> Repository { get; }

        public IQuery<TestDomain> Query { get; }
        public IQueryFiltered<TestDomain, TestDomainFilters> QueryFiltered { get; }

        public DapperTestObjects()
        {
            using var context = new SQLiteContext(DomainTestingObject.GetConnectionString());
            context.Database.Migrate();

            DbConnection = new SqliteConnection(DomainTestingObject.GetConnectionString());
            DbSession = new DbSession(DbConnection);
            UnityOfWork = new UnitOfWork(DbSession);

            var repo = new TestDomainDapperRepository(DbSession);
            var queryhandler = new TestDomainDapperQueryHandler(repo);

            Repository = repo;
            Query = queryhandler;
            QueryFiltered = queryhandler;
        }

        public void Dispose()
        {
            DbConnection?.Dispose();
            DbSession?.Dispose();
            UnityOfWork?.Dispose();
            Repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
