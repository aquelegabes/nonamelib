namespace NoNameLib.Application.Dispatcher;

public sealed partial class Dispatcher :
    IDispatcher, IAsyncDispatcher
{
    private static readonly string _interfaceName = typeof(ICommand<>).Name;
    private readonly IServiceProvider _sp;

    public Dispatcher(
        IServiceProvider sp)
    {
        this._sp = sp;
    }

    public void Dispatch<TCommand>(
        TCommand command)
        where TCommand : class
    {
        string commandName = typeof(TCommand).Name;

        var handler = this._sp.GetService(typeof(ICommand<TCommand>)) as ICommand<TCommand>
            ?? throw new HandlerNotImplementedException(
                            message: $"Could not find an implementation of {_interfaceName}<{commandName}>");

        handler?.Handle(command);
    }

    public async Task Dispatch<TCommand>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : class
    {
        string commandName = typeof(TCommand).Name;

        var handler = this._sp.GetService(typeof(IAsyncCommand<TCommand>)) as IAsyncCommand<TCommand>
            ?? throw new HandlerNotImplementedException(
                            message: $"Could not find an implementation of {_interfaceName}<{commandName}>");

        await handler.Handle(command, cancellationToken);
    }
}
