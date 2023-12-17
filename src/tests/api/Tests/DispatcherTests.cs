namespace NoNameLib.Application.Tests
{
    public class DispatcherTests
    {
        private readonly InputObject input = new() { Name = "Gabriel" };

        [Fact]
        public void Dispatcher_Void_OK()
        {
            var dispatcherMock = MockObjects.GetDispatcher(input);

            Assert.NotNull(dispatcherMock);
            Assert.NotNull(dispatcherMock.Object);

            var dispatcher = dispatcherMock.Object;
            dispatcher.Dispatch(input);
            dispatcherMock.Verify(
                _ => _.Dispatch(input),
                Times.Once());

            Assert.True(true);
        }

        [Fact]
        public async Task AsyncResultDispatcher_Task_OK()
        {
            var dispatcherMock = MockObjects.GetAsyncDispatcher<InputObject, OutputObject>(input);

            Assert.NotNull(dispatcherMock);
            Assert.NotNull(dispatcherMock.Object);

            var dispatcher = dispatcherMock.Object;
            var output = await dispatcher.Dispatch(input, typeof(OutputObject), default);

            dispatcherMock.Verify(
                _ => _.Dispatch(input, typeof(OutputObject), default),
                Times.Once());

            Assert.NotNull(output);
            Assert.NotNull(output.GetResult<OutputObject>());
        }

        [Fact]
        public void ResultDispatcher_String_OK()
        {
            var dispatcherMock = MockObjects.GetDispatcher<InputObject, string>(input);

            Assert.NotNull(dispatcherMock);
            Assert.NotNull(dispatcherMock.Object);

            var dispatcher = dispatcherMock.Object;
            var output = dispatcher.Dispatch(input, typeof(string));

            dispatcherMock.Verify(
                _ => _.Dispatch(input, typeof(string)),
                Times.Once());

            Assert.NotNull(output);
            Assert.NotNull(output.GetResult<string>());
        }

        [Fact]
        public async Task AsyncDispatcher_Task_OK()
        {
            var dispatcherMock = MockObjects.GetAsyncDispatcher(input);

            Assert.NotNull(dispatcherMock);
            Assert.NotNull(dispatcherMock.Object);

            var dispatcher = dispatcherMock.Object;
            await dispatcher.Dispatch(input, default);
            dispatcherMock.Verify(
                _ => _.Dispatch(input, default),
                Times.Once());

            Assert.True(true);
        }
    }
}
