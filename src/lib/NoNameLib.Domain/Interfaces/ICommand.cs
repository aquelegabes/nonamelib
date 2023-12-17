namespace NoNameLib.Domain.Interfaces;

public interface IBaseCommand { }

public interface ICommand<TDomain, TResult> : IBaseCommand
    where TDomain : class
{
    TResult Handle(TDomain domain);
}

public interface ICommand<TDomain> : IBaseCommand
    where TDomain : class
{
    void Handle(TDomain domain);
}

public interface IAsyncCommand<TDomain, TResult> : IBaseCommand
    where TDomain : class
{
    Task<TResult> Handle(TDomain domain, CancellationToken cancellationToken = default);
}

public interface IAsyncCommand<TDomain> : IBaseCommand
    where TDomain : class
{
    Task Handle(TDomain domain, CancellationToken cancellationToken = default);
}