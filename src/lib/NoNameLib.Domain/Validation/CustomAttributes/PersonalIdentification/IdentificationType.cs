namespace NoNameLib.Domain.Validation;

public abstract class IdentificationType
{
    protected readonly string _identifier;

    protected IdentificationType(string identifier)
    {
        _identifier = identifier.Trim();
    }

    public abstract bool IsValid();
}
