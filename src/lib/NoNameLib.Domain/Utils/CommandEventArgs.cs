namespace NoNameLib.Domain.Utils;

public abstract class CommandEventArgs : EventArgs
{
    private readonly object _domain;

    protected CommandEventArgs(object domain)
    {
        this._domain = domain;
    }

    public TDomain GetDomain<TDomain>()
        where TDomain : class
    {
        return _domain as TDomain;
    }
}
