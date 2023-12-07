namespace NoNameLib.Domain.Interfaces;

public interface ICommand<TDomain>
    where TDomain : class
{
    TDomain Handle(TDomain domain);
}

public interface IAsyncCommand<TDomain>
    where TDomain : class
{
    Task<TDomain> HandleAsync(TDomain domain, CancellationToken cancellationToken = default);
}