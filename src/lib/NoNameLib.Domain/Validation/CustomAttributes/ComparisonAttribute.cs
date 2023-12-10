using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ComparisonAttribute : ValidationAttribute
{
    private readonly ComparisonType _comparisonType;
    private readonly string _fieldNameToCompare1;
    private readonly string _fieldNameToCompare2;
    private object _fullObject;

    public ComparisonAttribute(
        ComparisonType comparisonType,
        string fieldNameToCompare1,
        string fieldNameToCompare2 = null)
    {
        _comparisonType = comparisonType;
        _fieldNameToCompare1 = fieldNameToCompare1;
        _fieldNameToCompare2 = fieldNameToCompare2;
    }

    public override bool IsValid(object value)
    {
        object comparisonValue2 = null;

        if (value == null)
            return false;

        var comparisonValue1 = _fullObject
            .GetType()
            .GetProperties()
            .Single(prop => prop.Name.Equals(_fieldNameToCompare1))
            .GetValue(_fullObject)
            ?? throw new MemberAccessException($"Object does not contain a member named: {_fieldNameToCompare1}");

        if (!string.IsNullOrWhiteSpace(_fieldNameToCompare2))
        {
            comparisonValue2 = _fullObject
                .GetType()
                .GetProperties()
                .Single(prop => prop.Name.Equals(_fieldNameToCompare2))
                .GetValue(_fullObject)
                ?? throw new MemberAccessException($"Object does not contain a member named: {_fieldNameToCompare2}");
        }

        return BooleanResolver.ResolveConditional(
                value.GetType(),
                _comparisonType,
                value,
                comparisonValue1,
                comparisonValue2);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        _fullObject = validationContext.ObjectInstance;

        return IsValid(value) ?
            ValidationResult.Success
            : new ValidationResult("Could not resolve the comparison between requested values");
    }
}
