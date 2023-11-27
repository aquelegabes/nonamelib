namespace NoNameLib.Extensions.Dappper;

public sealed class UnitOfWork : IUnitOfWork
{
    internal readonly DbSession _dbSession;
    private bool disposedValue;

    public UnitOfWork(
        DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public void BeginTransaction()
    {
        _dbSession.Transaction = _dbSession.DbConnection.BeginTransaction();
        _dbSession.LastTransactionAffectedRows = 0;
    }

    public int Commit()
    {
        _dbSession.Transaction?.Commit();
        Dispose();
        return _dbSession.LastTransactionAffectedRows;
    }

    public void RollbackTransaction()
    {
        _dbSession.Transaction?.Rollback();
        Dispose();
        _dbSession.LastTransactionAffectedRows = 0;
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _dbSession.Transaction?.Dispose();
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
