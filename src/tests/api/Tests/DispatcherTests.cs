using NoNameLib.Application.Interfaces;

namespace NoNameLib.Application.Tests
{
    public class DispatcherTests
    {
        private readonly InputObject input = new() { Name = "Gabriel" };

        #region OK
        [Fact]
        public void Dispatcher_Handle_OK()
        {
            var commandMock = MockObjects.GetCommandMock<InputObject>();
            var spMock = MockObjects.GetServiceProviderMock<InputObject>(commandMock.Object);
            var dispatcher = new Dispatcher.Dispatcher(spMock.Object) as IDispatcher;

            dispatcher.Dispatch(input);

            spMock.Verify(_ =>
                _.GetService(typeof(ICommand<InputObject>))
                , Times.Once());
            commandMock.Verify(_ =>
                _.Handle(input)
                , Times.Once());
        }

        [Fact]
        public async Task AsyncResultDispatcher_Handle_OK()
        {
            var commandMock = MockObjects.GetAsyncCommandOutputMock<InputObject, OutputObject>();
            var spMock = MockObjects.GetServiceProviderMock<InputObject, OutputObject>(
                commandMock.Object, true);
            var dispatcher = new Dispatcher.Dispatcher(spMock.Object) as IAsyncResultDispatcher;

            var result = await dispatcher.Dispatch(input, typeof(OutputObject));

            Assert.NotNull(result);
            Assert.NotNull(result.GetResult<OutputObject>());

            spMock.Verify(_ =>
                _.GetService(typeof(IAsyncCommand<InputObject, OutputObject>))
                , Times.Once());
            commandMock.Verify(_ =>
                _.Handle(input, default)
                , Times.Once());
        }

        [Fact]
        public void ResultDispatcher_Handle_OK()
        {
            var commandMock = MockObjects.GetCommandOutputMock<InputObject, OutputObject>();
            var spMock = MockObjects.GetServiceProviderMock<InputObject, OutputObject>(commandMock.Object);
            var dispatcher = new Dispatcher.Dispatcher(spMock.Object) as IResultDispatcher;

            var result = dispatcher.Dispatch(input, typeof(OutputObject));

            Assert.NotNull(result);
            Assert.NotNull(result.GetResult<OutputObject>());

            spMock.Verify(_ =>
                _.GetService(typeof(ICommand<InputObject, OutputObject>))
                , Times.Once());
            commandMock.Verify(_ =>
                _.Handle(input)
                , Times.Once());
        }

        [Fact]
        public async Task AsyncDispatcher_Task_OK()
        {
            var commandMock = MockObjects.GetAsyncCommandMock<InputObject>();
            var spMock = MockObjects.GetServiceProviderMock<InputObject>(commandMock.Object, true);
            var dispatcher = new Dispatcher.Dispatcher(spMock.Object) as IAsyncDispatcher;

            await dispatcher.Dispatch(input);

            spMock.Verify(_ =>
                _.GetService(typeof(IAsyncCommand<InputObject>))
                , Times.Once());
            commandMock.Verify(_ =>
                _.Handle(input, default)
                , Times.Once());
        }

        #endregion OK
    }
}
