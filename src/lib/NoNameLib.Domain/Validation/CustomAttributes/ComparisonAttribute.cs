using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ComparisonAttribute : ValidationAttribute
{
    private readonly BooleanResolver.ComparisonType _comparisonType;

    private readonly string _fieldNameToCompare;

    private object _fullObject;

    public ComparisonAttribute(
        BooleanResolver.ComparisonType comparisonType,
        string fieldNameToCompare)
    {
        _comparisonType = comparisonType;
        _fieldNameToCompare = fieldNameToCompare;
    }

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return false;
        }

        var comparisonValue = _fullObject
            .GetType()
            .GetProperties()
            .Single(prop => prop.Name.Equals(_fieldNameToCompare))
            .GetValue(_fullObject)
            ?? throw new MemberAccessException($"Object does not contain a member named: {_fieldNameToCompare}");

        return value.GetType() != comparisonValue.GetType()
            ? throw new InvalidOperationException("Both fields must have the same type!")
            : BooleanResolver.ResolveConditional(
                value.GetType(),
                _comparisonType,
                value,
                comparisonValue);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        _fullObject = validationContext.ObjectInstance;
        return IsValid(value) ? ValidationResult.Success : new ValidationResult("Could not resolve the comparison between requested values");
    }
}
