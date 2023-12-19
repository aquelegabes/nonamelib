using NoNameLib.Domain.Validation.PersonalIdentification.Brazil;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoNameLib.Domain.Tests.Mock;

[Table(nameof(TestDomain))]
public class TestDomain : IDomain<string>
{
    private Guid _id;

    [Key]
    public string Id
    {
        get { return _id.ToString(); }
        set { _id = Guid.Parse(value); }
    }

    [Required(AllowEmptyStrings = false)]
    [MutableDataMember]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    public DateTime BirthDate { get; set; }

    [NotNegative]
    [MutableDataMember]
    public int IntValue { get; set; }

    [PersonalIdentification(typeof(IdentificationCPF))]
    public string CPF { get; set; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    [Comparison(
        ComparisonType.GreaterThan,
        nameof(ContractDate))]
    public DateTime BeginDate { get; set; }

    [Required]
    [NotDefault]
    [DataType(DataType.DateTime)]
    public DateTime ContractDate { get; set; }

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