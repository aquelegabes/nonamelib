using NoNameLib.Domain.Validation.PersonalIdentification.Brazil;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoNameLib.Domain.Tests.PlayTest;

[Table(nameof(TestDomain))]
public class TestDomain : IDomain<string>
{
    private readonly Guid _id;

    [Key]
    public string Id
    {
        get { return _id.ToString(); }
        init { _id = Guid.Parse(value); }
    }

    [Required(AllowEmptyStrings = false)]
    [MutableDataMember]
    [MaxLength(100)]
    public string FullName { get; init; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    public DateTime BirthDate { get; init; }

    [NotNegative]
    [MutableDataMember]
    public int IntValue { get; init; }

    [PersonalIdentification(typeof(IdentificationCPF))]
    public string CPF { get; init; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    [Comparison(
        ComparisonType.GreaterThan,
        nameof(ContractDate))]
    public DateTime BeginDate { get; init; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    public DateTime ContractDate { get; init; }

    public TestDomain(
        string fullName,
        DateTime birthDate)
    {
        _id = Guid.NewGuid();
        FullName = fullName;
        BirthDate = birthDate;
    }

    public TestDomain() { }
}