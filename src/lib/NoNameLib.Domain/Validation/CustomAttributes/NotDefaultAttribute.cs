using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

/// <summary>
/// Specifies that a value must not be a default system value.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NotDefaultAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        var defaultValue = Activator.CreateInstance(value.GetType());

        return !value.Equals(defaultValue) || value == null;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        bool valid = IsValid(value);

        return valid ? ValidationResult.Success : new ValidationResult("Value must not be a default system value.");
    }
}
