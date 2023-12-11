using NoNameLib.Api.Controllers;
using NoNameLib.Domain.Interfaces;

namespace NoNameLib.Api.Commands
{
    public class OutputTest
    {
        public string Name { get; set; }
        public string Id => Guid.NewGuid().ToString();
    }

    public class OutputTestCommandHandler :
        ICommand<InputTest>,
        ICommand<InputTest, string>,
        IAsyncCommand<InputTest, OutputTest>
    {
        public void Handle(InputTest domain)
        {
            Console.WriteLine("ok");
        }

        public async Task<OutputTest> Handle(InputTest domain, CancellationToken cancellationToken)
        {
            await Task.Delay(300, cancellationToken);
            return new() { Name = domain.Name };
        }

        string ICommand<InputTest, string>.Handle(InputTest domain)
        {
            return "ok";
        }
    }
}
