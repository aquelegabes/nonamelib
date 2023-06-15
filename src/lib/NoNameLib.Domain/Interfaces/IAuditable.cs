namespace NoNameLib.Domain.Interfaces;
public interface IAuditable<TAuditKey, TData>
    : IDomain<TAuditKey>
    where TData : class
{
    TData ModifiedData { get; }
    TData OriginalData { get; }
    DateTime EventDate { get; }
    EventType EventType { get; }
}