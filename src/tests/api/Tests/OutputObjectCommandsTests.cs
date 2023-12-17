namespace NoNameLib.Application.Tests
{
    public class OutputObjectCommandsTests
    {
        private readonly OutputObjectCommandHandler OutputObjectCommandHandler = new();
        private readonly InputObject input = new() { Name = "Gabriel" };

        [Fact]
        public void Handler_Void_OK()
        {
            var handler = OutputObjectCommandHandler as ICommand<InputObject>;

            Assert.NotNull(handler);

            handler.Handle(input);

            Assert.True(true);
        }

        [Fact]
        public async Task AsyncHandler_Task_OK()
        {
            var handler = OutputObjectCommandHandler as IAsyncCommand<InputObject, OutputObject>;

            Assert.NotNull(handler);

            var output = await handler.Handle(input);

            Assert.NotNull(output);
        }

        [Fact]
        public void Handler_String_OK()
        {
            var handler = OutputObjectCommandHandler as ICommand<InputObject, string>;

            Assert.NotNull(handler);

            var outputString = handler.Handle(input);

            Assert.False(string.IsNullOrWhiteSpace(outputString));
        }
    }
}
