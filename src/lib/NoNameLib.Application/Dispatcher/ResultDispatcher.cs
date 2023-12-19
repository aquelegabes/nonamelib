#pragma warning disable CS8600, CS1066

using NoNameLib.Domain.Exceptions;

namespace NoNameLib.Application.Dispatcher;

public sealed partial class Dispatcher :
    IResultDispatcher, IAsyncResultDispatcher
{
    private static readonly string _asyncInterfaceName = typeof(IAsyncCommand<>).Name;
    private static Type GetCommandResultDispatcherServiceType(
        Type commandType,
        Type resultType,
        bool async = false)
    {
        return !async ?
            typeof(ICommand<,>).MakeGenericType(commandType, resultType)
            : typeof(IAsyncCommand<,>).MakeGenericType(commandType, resultType);
    }

    DispatchResult IResultDispatcher.Dispatch<TCommand>(
        TCommand command,
        Type resultType)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (resultType is null)
            throw new ArgumentNullException(nameof(resultType));

        string commandName = typeof(TCommand).Name;
        var serviceType = GetCommandResultDispatcherServiceType(typeof(TCommand), resultType);

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
        var serviceType = GetCommandResultDispatcherServiceType(typeof(TCommand), resultType, true);

        var handler = this._sp.GetService(serviceType)
            ?? throw new HandlerNotImplementedException(
                            message: "Could not find an implementation of type:" +
                                    $"{_asyncInterfaceName}<{commandName},{resultType.Name}>");

        var handleMethod = serviceType.GetMethod("Handle");
        var taskType = typeof(Task<>).MakeGenericType(resultType);

        if (taskType != handleMethod?.ReturnType)
        {
            throw new UnexpectedTypeException(
                        message: "Expected a handler that returned: " +
                                $"\"{taskType.Name}\" type but received one returning: \"{handleMethod?.ReturnType.Name}\" type.");
        }

        var result = (object)await (dynamic)handleMethod?.Invoke(handler, new object[] { command, cancellationToken });

        return new DispatchResult(result, resultType);
    }
}