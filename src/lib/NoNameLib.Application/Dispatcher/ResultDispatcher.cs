using NoNameLib.Domain.Exceptions;

namespace NoNameLib.Application.Dispatcher;

public sealed partial class Dispatcher :
    IResultDispatcher, IAsyncResultDispatcher
{
    private static readonly string _asyncInterfaceName = typeof(IAsyncCommand<>).Name;
    private static Type GetServiceType(Type commandType, Type ResultType, bool async = false) => !async ?
        typeof(ICommand<,>).MakeGenericType(commandType, ResultType)
        : typeof(IAsyncCommand<,>).MakeGenericType(commandType, ResultType);

    DispatchResult IResultDispatcher.Dispatch<TCommand>(
        TCommand command,
        Type resultType)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (resultType is null)
            throw new ArgumentNullException(nameof(resultType));

        string commandName = typeof(TCommand).Name;
        var serviceType = GetServiceType(typeof(TCommand), resultType);

        var handler = this._sp.GetService(serviceType)
            ?? throw new HandlerNotImplementedException(
                            message: "Could not find an implementation of type: " +
                                    $" {_asyncInterfaceName}<{commandName},{resultType.Name}>");

        var handleMethod = serviceType.GetMethod("Handle");
        var result = handleMethod?.Invoke(handler, new object[] { command });

        return new DispatchResult(result, resultType);
    }

    async Task<DispatchResult> IAsyncResultDispatcher.Dispatch<TCommand>(
        TCommand command,
        Type resultType,
        CancellationToken cancellationToken = default)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (resultType is null)
            throw new ArgumentNullException(nameof(resultType));

        string commandName = typeof(TCommand).Name;
        var serviceType = GetServiceType(typeof(TCommand), resultType, true);

        var handler = this._sp.GetService(serviceType)
            ?? throw new HandlerNotImplementedException(
                            message: "Could not find an implementation of type:" +
                                    $"{_asyncInterfaceName}<{commandName},{resultType.Name}>");

        var handleMethod = serviceType.GetMethod("Handle");
        var taskType = typeof(Task<>).MakeGenericType(resultType);

        if (taskType != handleMethod?.ReturnType)
            throw new UnexpectedTypeException(
                        message: "Expected a handler that returned: " +
                                $"\"{taskType.Name}\" type but received one returning: \"{handleMethod?.ReturnType.Name}\" type.");

        var result = (object)await (dynamic)handleMethod?.Invoke(handler, new object[] { command, cancellationToken });

        return new DispatchResult(result, resultType);
    }
}