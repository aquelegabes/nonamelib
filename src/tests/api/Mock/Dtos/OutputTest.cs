namespace NoNameLib.Api.Tests.Mock.Dtos;
public class OutputTest
{
    public string? Name { get; set; }
    public string Id = Guid.NewGuid().ToString();
}