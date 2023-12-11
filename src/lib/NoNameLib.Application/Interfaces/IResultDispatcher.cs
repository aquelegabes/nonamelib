namespace NoNameLib.Application.Interfaces;

public interface IResultDispatcher
{
    DispatchResult Dispatch<TCommand>(
        TCommand command,
        Type resultType)
        where TCommand : class;
}
