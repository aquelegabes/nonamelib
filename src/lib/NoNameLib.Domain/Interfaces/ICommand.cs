namespace NoNameLib.Domain.Interfaces;

public abstract class Command
{
    protected event EventHandler<CommandEventArgs> AfterHandle;
    protected event EventHandler<CommandEventArgs> BeforeHandle;

    protected void OnBeforeHandle(CommandEventArgs e)
    {
        BeforeHandle?.Invoke(this, e);
    }

    protected void OnAfterHandle(CommandEventArgs e)
    {
        AfterHandle?.Invoke(this, e);
    }

    public void AddNotificationsBeforeHandle(
        params INotification[] notifications)
    {
        foreach (var notification in notifications)
        {
            BeforeHandle += notification.Notify;
        }
    }

    public void AddNotificationsAfterHandle(
        params INotification[] notifications)
    {
        foreach (var notification in notifications)
        {
            AfterHandle += notification.Notify;
        }
    }
}

public interface ICommand<TDomain>
    where TDomain : class
{
    TDomain Handle(TDomain domain);
}

public interface IAsyncCommand<TDomain>
    where TDomain : class
{
    Task<TDomain> HandleAsync(TDomain domain, CancellationToken cancellationToken = default);
}