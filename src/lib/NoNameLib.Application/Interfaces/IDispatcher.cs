namespace NoNameLib.Application.Interfaces;

public interface IDispatcher
{
    void Dispatch<TCommand>(
        TCommand command)
        where TCommand : class;
}
