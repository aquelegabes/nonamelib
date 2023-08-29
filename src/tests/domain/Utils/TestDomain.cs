using NoNameLib.Domain.Validation.PersonalIdentification.Brazil;
using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Tests.Utils;

internal class TestDomain : IDomain<string>
{
    private readonly Guid _id;

    [Key]
    public string Id => _id.ToString();

    [Required(AllowEmptyStrings = false)]
    [MaxLength(100)]
    public string FullName { get; init; }

    [Required]
    [NotDefault]
    public DateTime BirthDate { get; init; }

    [NotNegative]
    public int IntValue { get; init; }

    [PersonalIdentification(typeof(IdentificationCPF))]
    public string CPF { get; init; }

    [Required]
    [NotDefault]
    [Comparison(
        BooleanResolver.ComparisonType.GreaterThan,
        nameof(ContractDate))]
    public DateTime BeginDate { get; init; }

    [Required]
    [NotDefault]
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