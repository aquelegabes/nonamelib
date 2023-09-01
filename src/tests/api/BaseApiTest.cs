namespace NoNameLib.Api.Tests;

public class BaseApiTest
{
    [Fact]
    public void Passing_GET_One()
    {
        var result = apiController<ApiTest>.Get();
        var objectResult = result as ObjectResult;
        var modelResult = objectResult.Value.FromJson<ApiTestModel>();

        Assert.NotNull(result);
        Assert.NotNull(objectResult);
        Assert.NotNull(modelResult);
    }
    
    [Fact]
    public void Passing_GET_Many()
    {
        var result = apiController<ApiTest>.Get();
        var objectResult = result as ObjectResult;
        var modelResult = objectResult.Value.FromJson<IEnumerable<ApiTestModel>>();

        Assert.NotNull(result);
        Assert.NotNull(objectResult);
        Assert.NotNull(modelResult);
        Assert.NotEmpty(modelResult);
    }
        
    [Fact]
    public void Passing_POST()
    {

    }
    
    [Fact]
    public void Passing_PUT()
    {

    }
    [Fact]
    public void Passing_DELETE()
    {

    }
}