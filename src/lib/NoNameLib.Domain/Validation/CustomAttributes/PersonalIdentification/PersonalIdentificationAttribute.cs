using NoNameLib.Domain.Utils.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class PersonalIdentificationAttribute : ValidationAttribute
{
    private readonly Type _identificationType;

    public PersonalIdentificationAttribute(
        Type IdentificationType)
    {
        _identificationType = IdentificationType;
    }

    public override bool IsValid(object value)
    {
        if (value is null)
            return true;

        if (!_identificationType.IsAssignableTo(typeof(IdentificationType)))
            throw new InvalidIdentificationTypeException("A valid identification type must be specified.");

        if (value is not string identification)
            throw new ArgumentException("Identification must be a string type.");

        var idType = Activator.CreateInstance(_identificationType, identification) as IdentificationType;

        return idType.IsValid();
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return IsValid(value) ? ValidationResult.Success : new ValidationResult("Identification is not valid!");
    }
}