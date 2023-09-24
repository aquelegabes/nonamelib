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

        foreach (var propInfo in mutablePropInfos)
        {
            var newPropValue = propInfo.GetValue(newerDomain);

            if (newPropValue is null
                || newPropValue == default
                || !propInfo.GetCustomAttributes(typeof(MutableDataMemberAttribute), true).Any())
            {
                continue;
            }

            propInfo.SetValue(existingDomain, newPropValue);
        }
    }
}
