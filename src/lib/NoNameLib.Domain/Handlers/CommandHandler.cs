namespace NoNameLib.Domain.Handlers;

public sealed class CommandHandler
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDomain Send<TDomain>(
        Type commandType,
        TDomain domain)
        where TDomain : class
    {
        var command = _serviceProvider.GetService(commandType) as ICommand<TDomain>;
        return command.Send(domain);
    }

    public async Task<TDomain> SendAsync<TDomain>(
        Type commandType,
        TDomain domain,
        CancellationToken cancellationToken = default)
    {
        var command = _serviceProvider.GetService(commandType) as ICommandAsync<TDomain>;
        return await command.SendAsync(domain, cancellationToken);
    }
}

public sealed class CommandHandler<TDomain>
    where TDomain : class
{
    private readonly ICommand<TDomain> _command;

    public CommandHandler(ICommand<TDomain> command)
    {
        _command = command;
    }

    public TDomain Send(
        TDomain domain)
    {
        return _command.Send(domain);
    }
}

public sealed class CommandHandlerAsync<TDomain>
    where TDomain : class
{
    private readonly ICommandAsync<TDomain> _command;

    public CommandHandlerAsync(ICommandAsync<TDomain> command)
    {
        _command = command;
    }

    public async Task<TDomain> Send(TDomain domain)
    {
        return await _command.SendAsync(domain);
    }
}