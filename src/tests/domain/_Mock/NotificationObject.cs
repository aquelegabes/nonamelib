namespace NoNameLib.Domain.Tests.Mock;

public class NotificationObject : IDomain<string>
{
    public string Id { get; set; }
    public object? Object { get; set; }
}