namespace NoNameLib.Application.Interfaces;

public interface IAsyncDispatcher
{
    Task Dispatch<TCommand>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : class;
}