namespace NoNameLib.Domain.Tests.Mock;

public class NotificationRepository : IRepository<NotificationObject>
{
    private readonly List<NotificationObject> _notifications;

    internal NotificationRepository(
        DomainTestingObject testList)
    {
        _notifications = testList.NotificationObjectsList;
    }

    public void Delete(NotificationObject domain)
    {
        _notifications.Remove(domain);
    }

    public void Dispose()
    {
    }

    public int SaveChanges(NotificationObject domain, TransactionType eventType)
    {
        _notifications.Add(domain);
        return 1;
    }
}
