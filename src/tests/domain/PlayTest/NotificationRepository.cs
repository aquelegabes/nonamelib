﻿namespace NoNameLib.Domain.Tests.PlayTest;

public class NotificationRepository : IRepository<NotificationObject>
{
    private readonly List<NotificationObject> _notifications;

    public NotificationRepository(
        MainTestingObject testList)
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
        ValidationHandler.Validate(domain);
        _notifications.Add(domain);
        return 1;
    }
}