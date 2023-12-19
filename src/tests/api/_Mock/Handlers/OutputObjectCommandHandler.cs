namespace NoNameLib.Application.Tests.Mock.Handlers;

public class OutputObjectCommandHandler :
    ICommand<InputObject>,
    ICommand<InputObject, string>,
    IAsyncCommand<InputObject>,
    IAsyncCommand<InputObject, OutputObject>
{
    public void Handle(InputObject domain)
    {
        Console.WriteLine("ok");
    }

    public async Task<OutputObject> Handle(InputObject domain, CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return new() { Name = domain.Name };
    }

    string ICommand<InputObject, string>.Handle(InputObject domain)
    {
        return "ok";
    }

    async Task IAsyncCommand<InputObject>.Handle(InputObject domain, CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        Console.WriteLine("ok");
    }
}
