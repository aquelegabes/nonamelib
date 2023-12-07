namespace NoNameLib.Domain.Abstract;

public abstract class Notifiable
{
    protected event EventHandler<NotifiableEventArgs> AfterHandle;
    protected event EventHandler<NotifiableEventArgs> BeforeHandle;

    protected void OnBeforeHandle(NotifiableEventArgs e)
    {
        BeforeHandle?.Invoke(this, e);
    }

    protected void OnAfterHandle(NotifiableEventArgs e)
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
