namespace NoNameLib.Application.Interfaces;

public interface IAsyncResultDispatcher
{
    Task<DispatchResult> Dispatch<TCommand>(
        TCommand command,
        Type resultType,
        CancellationToken cancellationToken = default)
        where TCommand : class;
}
