namespace NoNameLib.Extensions.Dappper;

public sealed class DbSession : IDisposable
{
    private bool disposedValue;

    public IDbConnection DbConnection { get; internal set; }
    public IDbTransaction? Transaction { get; internal set; }
    public Guid TransactionId { get; }
    public int LastTransactionAffectedRows { get; internal set; }

    public DbSession(
        IDbConnection dbConnection)
    {
        TransactionId = Guid.NewGuid();
        this.DbConnection = dbConnection;
        DbConnection.Open();
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                DbConnection?.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}