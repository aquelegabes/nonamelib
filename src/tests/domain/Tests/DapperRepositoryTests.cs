using NoNameLib.Domain.Enums;
using NoNameLib.Domain.Extensions;

namespace NoNameLib.Domain.Tests.Tests;

public partial class DapperRepositoryTests
{
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
