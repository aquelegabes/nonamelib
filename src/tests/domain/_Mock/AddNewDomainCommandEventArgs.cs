namespace NoNameLib.Domain.Tests.Mock;

internal class AddNewDomainCommandEventArgs : NotifiableEventArgs
{
    public AddNewDomainCommandEventArgs(object domain) : base(domain)
    {
    }
}
