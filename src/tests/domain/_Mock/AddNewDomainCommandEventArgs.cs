#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

namespace NoNameLib.Domain.Tests.Mock;

internal class AddNewDomainCommandEventArgs : NotifiableEventArgs
{
    public AddNewDomainCommandEventArgs(object domain) : base(domain)
    {
    }
}
