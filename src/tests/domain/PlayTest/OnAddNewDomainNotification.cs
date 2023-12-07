using NoNameLib.Domain.Enums;

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
        object sender, NotifiableEventArgs e)
    {
        _notificationRepository.SaveChanges(
            new NotificationObject()
            {
                Id = Guid.NewGuid().ToString(),
                Object = e.GetData()
            },TransactionType.Create);
    }
}