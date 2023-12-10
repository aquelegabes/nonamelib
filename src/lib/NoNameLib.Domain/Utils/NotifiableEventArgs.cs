namespace NoNameLib.Domain.Utils;

public abstract class NotifiableEventArgs : EventArgs
{
    private readonly object _data;

    protected NotifiableEventArgs(object data)
    {
        this._data = data;
    }

    public T GetData<T>()
        where T : class
    {
        return _data as T;
    }

    public object GetData()
    {
        return _data;
    }
}