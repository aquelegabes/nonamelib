using NoNameLib.Extensions.Dappper;

namespace NoNameLib.Extensions;

public static class UnitOfWorkExtensions
{
    public static void CommitDo(
        this UnitOfWork _unitOfWork,
        Action action)
    {
        _unitOfWork._dbSession.Transaction?.Commit();
        action?.Invoke();
        _unitOfWork.Dispose();
    }
}
