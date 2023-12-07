using AutoMapper;
using NoNameLib.Domain.Interfaces;
using static NoNameLib.Domain.Enums.TransactionType;

namespace NoNameLib.Api.Commands;

public abstract class BaseAsyncCreateCommandHandler<TCreateModel, TDomain> :
    IAsyncCommand<TCreateModel>
    where TCreateModel : class
    where TDomain : class
{
    protected readonly IMapper _mapper;
    protected readonly IAsyncRepository<TDomain> _repository;
    protected readonly IAsyncUnitOfWork _unitOfWork;

    protected BaseAsyncCreateCommandHandler(
        IMapper mapper,
        IAsyncRepository<TDomain> repository,
        IAsyncUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public virtual async Task<TCreateModel> HandleAsync(
        TCreateModel model,
        CancellationToken cancellationToken = default)
    {
        var domain = _mapper.Map<TDomain>(model);
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var savedRows = await _repository.SaveChangesAsync(domain, Create, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return _mapper.Map<TCreateModel>(domain);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

public abstract class BaseCreateCommandHandler<TModel, TDomain> :
    ICommand<TModel>
    where TModel : class
    where TDomain : class
{
    protected readonly IRepository<TDomain> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseCreateCommandHandler(
        IRepository<TDomain> repository,
        IUnitOfWork unitOfWork,
        IMapper _mapper)
    {
        this._repository = repository;
        this._unitOfWork = unitOfWork;
        this._mapper = _mapper;
    }

    public virtual TModel Handle(TModel model)
    {
        var domain = _mapper.Map<TDomain>(model);
        _unitOfWork.BeginTransaction();
        try
        {
            var savedRows = _repository.SaveChanges(domain, Create);
            _unitOfWork.Commit();
            return _mapper.Map<TModel>(domain);
        }
        catch
        {
            _unitOfWork.RollbackTransaction();
            throw;
        }
    }
}
