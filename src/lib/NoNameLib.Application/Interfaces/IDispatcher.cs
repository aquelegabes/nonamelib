namespace NoNameLib.Application.Interfaces;

public interface IResultDispatcher
{
    DispatchResult Dispatch<TCommand>(
        TCommand command,
        Type resultType)
        where TCommand : class;
}

public interface IAsyncResultDispatcher
{
    Task<DispatchResult> Dispatch<TCommand>(
        TCommand command,
        Type resultType,
        CancellationToken cancellationToken = default)
        where TCommand : class;
}

public interface IDispatcher
{
    void Dispatch<TCommand>(
        TCommand command)
        where TCommand : class;
}

public interface IAsyncDispatcher
{
    Task Dispatch<TCommand>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : class;
}