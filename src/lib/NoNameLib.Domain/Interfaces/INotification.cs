namespace NoNameLib.Domain.Interfaces
{
    public interface INotification
    {
        void Notify(object sender, NotifiableEventArgs e);
    }

    public interface INotification<T>
    {
        T Notify(object sender, NotifiableEventArgs e);
    }
}
