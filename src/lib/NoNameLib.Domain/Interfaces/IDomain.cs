namespace NoNameLib.Domain.Interfaces;

public interface IDomain<TKey>
{
    TKey Id { get; init; }
}