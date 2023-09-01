namespace NoNameLib.Domain.Records;
public record Auditable<TData>
    where TData : class
{
    public TData ModifiedData { get; set; }
    public TData OriginalData { get; set; }
    public EventType EventType { get; set; }
    public DateTime AuditDate { get; set; }
}