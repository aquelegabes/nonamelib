using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NoNameLib.Domain.Enums;
using NoNameLib.Domain.Extensions;
using NoNameLib.Domain.Tests.SQLite;
using NoNameLib.Extensions.Dappper;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace NoNameLib.Domain.Tests.Tests;

public class DapperRepositoryTests
{
    public class DapperTestObjects : IDisposable
    {
        public IDbConnection DbConnection { get; }
        public DbSession DbSession { get; }
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
        }
    }

    [Fact]
    public void DapperRepository_SaveChanges_Create_OK()
    {
        var dapperObject = new DapperTestObjects();
        var testingObject = new DomainTestingObject();
        var entity = testingObject.TestDomainList.First();

        dapperObject.UnityOfWork.BeginTransaction();
        var affectedRows = dapperObject.Repository.SaveChanges(entity, TransactionType.Create);
        var result = dapperObject.UnityOfWork.Commit();

        Assert.True(affectedRows >= 1);
        Assert.True(result >= 1);
    }

    [Fact]
    public void DapperRepository_SaveChanges_Update_OK()
    {
        var dapperObject = new DapperTestObjects();
        var updatedEntity = new TestDomain()
        {
            FullName = "Thiago Elias"
        };

        var elementToUpdate = dapperObject.Query.Get().First();
        elementToUpdate.Update(updatedEntity);

        dapperObject.UnityOfWork.BeginTransaction();
        int affectedRows = dapperObject.Repository.SaveChanges(elementToUpdate, TransactionType.Update);
        int result = dapperObject.UnityOfWork.Commit();

        Assert.True(affectedRows >= 1);
        Assert.True(result >= 1);
    }

    [Fact]
    public void DapperRepositoryQuery_OK()
    {
        using var dapperObject = new DapperTestObjects();

        var elements = dapperObject.Query.Get().ToList();

        Assert.NotNull(elements);
        Assert.NotEmpty(elements);
        Assert.True(elements.Count >= 1);
    }

    [Theory]
    [InlineData("Gabriel Santos")]
    public void DapperRepositoryQueryFiltered_OK(
        string name)
    {
        using var dapperObject = new DapperTestObjects();

        var elements = dapperObject
            .QueryFiltered
            .Get(new TestDomainFilters() { FullName = name })
            .ToList();

        Assert.NotNull(elements);
        Assert.NotEmpty(elements);
    }


}
