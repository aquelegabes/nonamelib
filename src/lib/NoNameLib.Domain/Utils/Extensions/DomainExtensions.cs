using NoNameLib.Domain.Validation;

namespace NoNameLib.Domain.Utils.Extensions;

public static class DomainExtensions
{
    public static void Update<TKey>(
        this IDomain<TKey> existingDomain,
        IDomain<TKey> newerDomain)
    {
        var mutablePropInfos =
            existingDomain
            .GetType()
            .GetProperties()
            .Where(prop => prop.SetMethod != null);

        foreach ( var propInfo in mutablePropInfos )
        {
            var newPropValue = propInfo.GetValue(newerDomain);
            propInfo.SetValue(existingDomain, newPropValue);
        }

        ValidationHandler.Validate(existingDomain);
    }
}
