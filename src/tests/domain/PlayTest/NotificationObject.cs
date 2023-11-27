namespace NoNameLib.Domain.Tests.PlayTest;

public class NotificationObject : IDomain<string>
{
    public string Id { get; set; }
    public object? Object { get; set; }
}