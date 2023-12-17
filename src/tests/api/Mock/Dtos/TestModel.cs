namespace NoNameLib.Api.Tests.Mock.Dtos;

public class TestModel : IDomain<string>
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime ContractDate { get; set; }
}
