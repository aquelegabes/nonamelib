using AutoMapper;
using NoNameLib.Domain.Interfaces;
using static NoNameLib.Domain.Enums.TransactionType;

namespace NoNameLib.Api.Commands;

public class BaseDeleteCommandHandler<TDeleteModel, TDomain, TKey> :
    ICommand<TDeleteModel>
    where TDeleteModel : class, IDomain<TKey>
    where TDomain : class, IDomain<TKey>
{
    protected readonly IRepository<TDomain> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseDeleteCommandHandler(
        IRepository<TDomain> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public void Handle(TDeleteModel model)
    {
        var domain = _mapper.Map<TDomain>(model);
        _unitOfWork.BeginTransaction();

        try
        {
            _repository.SaveChanges(domain, Delete);
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.RollbackTransaction();
            throw;
        }
    }
}

public class BaseAsyncDeleteCommandHandler<TDeleteModel, TDomain, TKey> :
    IAsyncCommand<TDeleteModel>
    where TDeleteModel : class, IDomain<TKey>
    where TDomain : class, IDomain<TKey>
{
    protected readonly IAsyncRepository<TDomain> _repository;
    protected readonly IAsyncUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseAsyncDeleteCommandHandler(
        IAsyncRepository<TDomain> repository,
        IAsyncUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(
        TDeleteModel model,
        CancellationToken cancellationToken)
    {
        var domain = _mapper.Map<TDomain>(model);
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _repository.SaveChangesAsync(domain, Delete, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}