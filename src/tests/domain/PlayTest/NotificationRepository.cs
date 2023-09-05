namespace NoNameLib.Domain.Tests.PlayTest;

public class NotificationRepository : IRepository<NotificationObject>
{
    private readonly List<NotificationObject> _notifications;

    public NotificationRepository(
        TestList testList)
    {
        _notifications = testList.NotificationObjectsList;
    }

    public void Delete(NotificationObject domain)
    {
        _notifications.Remove(domain);
    }

    public int SaveChanges(NotificationObject domain)
    {
        ValidationHandler.Validate(domain);
        _notifications.Add(domain);
        return 1;
    }
}
