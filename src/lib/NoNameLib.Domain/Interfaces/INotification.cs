namespace NoNameLib.Domain.Interfaces
{
    public interface INotification
    {
        void Notify(object sender, CommandEventArgs e);
    }
}
