using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NoNameLib.Api.Entities;

public class ApiTest : IDomain<string>
{
    private readonly Guid _guid;
    private readonly string _id;

    [JsonIgnore, IgnoreDataMember]
    public DateTime MinBirthDate => new(year: 1950, 1, 1);
    [JsonIgnore, IgnoreDataMember]
    public DateTime MaxBirthDate => DateTime.Today;

    [Key]
    public string Id
    {
        get { return _id; }
        init
        {
            if (value is null)
            {
                _guid = Guid.NewGuid();
                _id = _guid.ToString();
            }
            else
            {
                _guid = Guid.Parse(value);
                _id = value;
            }
        }
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsValid { get; set; } = false;

    [Required, NotDefault]
    [Comparison(ComparisonType.Between, nameof(MinBirthDate), nameof(MaxBirthDate))]
    public DateTime BirthDate { get; set; }
}
