namespace NoNameLib.Domain.Tests.PlayTest;

public class NotificationObject : IDomain<string>
{
    public string Id { get; init; }
    public object? Object { get; init; }
}