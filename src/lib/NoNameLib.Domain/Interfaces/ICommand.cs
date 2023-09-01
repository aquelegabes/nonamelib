namespace NoNameLib.Domain.Interfaces;

public abstract class Command
{
    protected event EventHandler<CommandEventArgs> CommandSent;
    public virtual void OnCommandSent(CommandEventArgs e)
    {
        CommandSent?.Invoke(this, e);
    }
}

public interface ICommand<TDomain>
    where TDomain : class
{
    TDomain Send(TDomain domain);
}

public interface ICommandAsync<TDomain> : IDisposable
{
    Task<TDomain> SendAsync(TDomain domain, CancellationToken cancellationToken = default);
}