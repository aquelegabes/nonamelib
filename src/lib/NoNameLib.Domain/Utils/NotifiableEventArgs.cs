namespace NoNameLib.Domain.Utils;

public abstract class NotifiableEventArgs : EventArgs
{
    private readonly object _data;

    protected NotifiableEventArgs(object data)
    {
        this._data = data;
    }

    public TDomain GetData<TDomain>()
        where TDomain : class
    {
        return _data as TDomain;
    }

    public object GetData()
    {
        return _data;
    }
}