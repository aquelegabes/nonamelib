namespace NoNameLib.Application.Tests.Mock.Dtos;
public class OutputObject
{
    public string? Name { get; set; }
    public string Id = Guid.NewGuid().ToString();
}