namespace NoNameLib.Domain.Utils;

public class DispatchResult
{
    private readonly object _resultObject;

    public DispatchResult(
        object result,
        Type resultDataType)
    {
        if (result is null)
        {
            throw new ArgumentNullException(
                        paramName: nameof(result),
                        message: "Result must no be null.");
        }

        if (resultDataType is null)
        {
            throw new ArgumentNullException(
                        paramName: nameof(resultDataType),
                        message: "Expected result type must no be null.");
        }

        if (resultDataType != result.GetType())
        {
            throw new UnexpectedTypeException(
                        message: "Result data type must match expected data type. " +
                                $"Result type: {_resultObject.GetType().Name}, " +
                                $"Expected type: {resultDataType.Name}");
        }

        this._resultObject = result;
    }

    public T GetResult<T>()
    {
        return (T)_resultObject;
    }
}
