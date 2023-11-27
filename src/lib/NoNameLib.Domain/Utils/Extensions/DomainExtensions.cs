using NoNameLib.Domain.Validation;

namespace NoNameLib.Domain.Utils.Extensions;

public static class DomainExtensions
{
    internal static void Update<TKey>(
        this IDomain<TKey> existingDomain,
        Type modelType,
        object model)
    {
        var mutablePropInfos =
            existingDomain
            .GetType()
            .GetProperties()
            .Where(prop => prop.SetMethod != null);

        foreach (var propInfo in mutablePropInfos)
        {
            var propInfoFromModel = modelType.GetProperty(propInfo.Name);
            var newPropValue = propInfoFromModel.GetValue(model);

            if (newPropValue is null
                || newPropValue == default
                || !propInfo.GetCustomAttributes(typeof(MutableDataMemberAttribute), true).Any())
            {
                continue;
            }

            propInfo.SetValue(existingDomain, newPropValue);
        }
    }

    public static void Update<TKey, TModel>(
        this IDomain<TKey> existingDomain,
        TModel model)
    {
        existingDomain.Update(typeof(TModel), model);
    }

    public static void Update<TKey>(
        this IDomain<TKey> existingDomain,
        IDomain<TKey> model)
    {
        existingDomain.Update(typeof(IDomain<TKey>), model);
    }
}
