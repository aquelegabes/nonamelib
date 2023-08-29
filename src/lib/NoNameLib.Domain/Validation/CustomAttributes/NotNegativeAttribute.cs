using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NotNegativeAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return true;

        var acceptedTypes = new[]
        {
            typeof(short),
            typeof(sbyte),
            typeof(byte),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
        };

        if (!acceptedTypes.Contains(value.GetType()))
            throw new InvalidAttributeUsageException();

        if (value is IComparable compare)
        {
            return compare.CompareTo(0) >= 0;
        }

        return false;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return IsValid(value) ? ValidationResult.Success : new ValidationResult("Value must not be negative.");
    }
}
