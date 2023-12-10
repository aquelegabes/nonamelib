namespace NoNameLib.Domain.Utils;

public enum OperatorType : int
{
    AND = 1,
    OR = 2
}
public enum ComparisonType : int
{
    LessThan = 1,
    GreaterThan = 2,
    EqualTo = 3,
    DifferentFrom = 4,
    Between = 5,
}

/// <summary>
/// Resolves a boolean operation using parameterized arguments.
/// </summary>
public static class BooleanResolver
{
    /// <summary>
    /// Resolve a condition between two objects.
    /// </summary>
    /// <param name="type">Type of the objects.</param>
    /// <param name="comparisonType">Condition to be resolved.</param>
    /// <param name="obj">Object to compare.</param>
    /// <param name="source1">First source object to be compared.</param>
    /// <param name="source2">Second source object to be compared.</param>
    /// <exception cref="ArgumentException">Invalid <paramref name="comparisonType"/>.</exception>
    /// <exception cref="ArgumentException">Comparison type is <see cref="ComparisonType.Between"/> <paramref name="source1"/> and <paramref name="source2"/> are null.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="obj"/> is null.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="source1"/> is null.</exception>
    /// <exception cref="TypeAccessException">Type does not implements <see cref="IComparable"/>.</exception>
    /// <returns>The result of the condition.</returns>
    public static bool ResolveConditional(
        Type type, ComparisonType comparisonType, object obj, object source1, object source2 = null)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type), "Type must not be null.");

        if (comparisonType == default)
            throw new ArgumentException("Invalid conditional.", nameof(comparisonType));

        if (obj is null)
            throw new ArgumentNullException(nameof(obj), "Object reference must not be null.");

        if (source1 is null)
            throw new ArgumentNullException(nameof(source1), "Object source must not be null.");

        if (comparisonType == ComparisonType.Between && source1 is null && source2 is null)
            throw new ArgumentException(
                        "When comparison type equals \"ComparisonType.Between\" both source objects must not be null");

        if (!type.GetInterfaces().Contains(typeof(IComparable)))
            throw new TypeAccessException($"Type : '{nameof(type)}' does not implements {nameof(IComparable)}.");

        if (obj.GetType() != source1.GetType())
            throw new InvalidOperationException("Object and its sources must be the same type.");

        if (source2 is not null && source2.GetType() != obj.GetType())
            throw new InvalidOperationException("Object and its sources must be the same type.");

        var comparableObj = (IComparable)obj;

        return comparisonType switch
        {
            ComparisonType.LessThan => comparableObj.CompareTo(source1) < 0,
            ComparisonType.GreaterThan => comparableObj.CompareTo(source1) > 0,
            ComparisonType.EqualTo => comparableObj.CompareTo(source1) == 0,
            ComparisonType.DifferentFrom => comparableObj.CompareTo(source1) != 0,
            ComparisonType.Between => comparableObj.CompareTo(source1) > 0 && comparableObj.CompareTo(source2) < 0,
            _ => false,
        };
    }

    /// <summary>
    /// Resolve a condition between two objects.
    /// </summary>
    /// <param name="conditional">Condition to be resolved.</param>
    /// <param name="obj">Object to compare.</param>
    /// <param name="source1">Source object to be compared.</param>
    /// <typeparam name="T">Type of the objects.</typeparam>
    /// <exception cref="ArgumentException">Invalid <paramref name="conditional"/>.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="obj"/> is null.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="source1"/> is null.</exception>
    /// <returns>The result of the condition.</returns>
    public static bool ResolveConditional<T>(
        ComparisonType conditional, T obj, T source1, T source2 = null)
        where T : class, IComparable
    {
        return ResolveConditional(typeof(T), conditional, obj, source1, source2);
    }

    /// <summary>
    /// Resolve a boolean operation using an <see cref="OperatorType"/>.
    /// </summary>
    /// <param name="operator">Operator to be resolved.</param>
    /// <param name="conditions">Conditions to resolve.</param>
    /// <returns>The operation result.</returns>
    /// <exception cref="ArgumentException">Invalid <paramref name="operator"/>.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="conditions"/> is null.</exception>
    public static bool ResolveOperation(
        OperatorType @operator, params bool[] conditions)
    {
        if (@operator == default)
            throw new ArgumentException("Invalid operator.", nameof(@operator));
        if (conditions?.Any() == false)
            throw new ArgumentNullException(
                        paramName: nameof(conditions),
                        message: "At least one condition must be specified.");

        bool? result = null;
        foreach (var condition in conditions)
        {
            result ??= condition;

            if (OperatorType.AND == @operator)
                result = result.Value && condition;
            else
                result = result.Value || condition;
        }
        return result.Value;
    }
}