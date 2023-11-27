namespace NoNameLib.Domain.Tests.PlayTest;

internal class OnAddNewDomainNotification : INotification
{
    private readonly IRepository<NotificationObject> _notificationRepository;

    public OnAddNewDomainNotification(
        IRepository<NotificationObject> notificationRepository)
    {
        this._notificationRepository = notificationRepository;
    }

    public void Notify(
        object sender, CommandEventArgs e)
    {
        _notificationRepository.SaveChanges(
            new NotificationObject()
            {
                Id = Guid.NewGuid().ToString(),
                Object = e.GetDomain()
            },TransactionType.Create);
    }
}