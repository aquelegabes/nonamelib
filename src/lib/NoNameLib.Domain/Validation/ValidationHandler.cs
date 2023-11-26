using System.ComponentModel.DataAnnotations;

namespace NoNameLib.Domain.Validation;

public static class ValidationHandler
{
    public static void Validate<TDomain>(
        TDomain domain)
        where TDomain : class
    {
        var propsToValidate =
            domain.GetType()
            .GetProperties()
            .Where(prop => prop.GetCustomAttributes(typeof(ValidationAttribute), true).Any());

        if (propsToValidate?.Any() != true)
            return;

        var validationContext = new ValidationContext(domain);
        var errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(domain, validationContext, errors, true);

        if (!valid && errors.Count > 1)
        {
            List<ValidationException> exceptions = new();

            Parallel.ForEach(errors, error => exceptions.Add(new ValidationException(error.ErrorMessage)));

            throw new AggregateException("Multiple validation errors has occurred, check inner exception for details.", exceptions);
        }

        if (!valid && errors.Count == 1)
            throw new ValidationException(errors[0].ErrorMessage);
    }
}
