namespace NoNameLib.Domain.Tests.Mock;

public class NotificationObject : IDomain<string>
{
    public string Id { get; set; } = string.Empty;
    public object? Object { get; set; }
}