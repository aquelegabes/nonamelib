using NoNameLib.Application.Interfaces;

namespace NoNameLib.Application.Tests.Mock
{
    public static class MockObjects
    {
        public static Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            var mock = new Mock<IUnitOfWork>();

            mock.Setup(uow => uow.BeginTransaction());
            mock.Setup(uow => uow.RollbackTransaction());
            mock.Setup(uow => uow.Commit());

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput>(
            object commandMockObject,
            bool async = false)
            where TInput : class
        {
            var mock = new Mock<IServiceProvider>();

            if (async)
                mock.Setup(sp => sp.GetService(typeof(IAsyncCommand<TInput>)))
                    .Returns(commandMockObject);
            else
                mock.Setup(sp => sp.GetService(typeof(ICommand<TInput>)))
                    .Returns(commandMockObject);

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput, TOutput>(
            object commandMockObject,
            bool async = false)
            where TInput : class
            where TOutput : class
        {
            var mock = new Mock<IServiceProvider>();

            if (async)
                mock.Setup(sp => sp.GetService(typeof(IAsyncCommand<TInput, TOutput>)))
                    .Returns(commandMockObject);
            else
                mock.Setup(sp => sp.GetService(typeof(ICommand<TInput, TOutput>)))
                    .Returns(commandMockObject);

            return mock;
        }

        public static Mock<ICommand<TInput>> GetCommandMock<TInput>()
            where TInput : class
        {
            var mock = new Mock<ICommand<TInput>>();

            mock.Setup(comm => comm.Handle(It.IsAny<TInput>()));

            return mock;
        }

        public static Mock<ICommand<TInput, TOutput>> GetCommandOutputMock<TInput, TOutput>()
            where TInput : class
            where TOutput : class
        {
            var mock = new Mock<ICommand<TInput, TOutput>>();

            var result = Activator.CreateInstance<TOutput>();

            mock.Setup(comm => comm.Handle(It.IsAny<TInput>()))
                .Returns(result);

            return mock;
        }

        public static Mock<IAsyncCommand<TInput>> GetAsyncCommandMock<TInput>()
            where TInput : class
        {
            var mock = new Mock<IAsyncCommand<TInput>>();

            mock.Setup(comm => comm.Handle(It.IsAny<TInput>(), default));

            return mock;
        }

        public static Mock<IAsyncCommand<TInput, TOutput>> GetAsyncCommandOutputMock<TInput, TOutput>()
            where TInput : class
            where TOutput : class
        {
            var mock = new Mock<IAsyncCommand<TInput, TOutput>>();

            var output = Activator.CreateInstance<TOutput>();
            var result = Task.FromResult(output);

            mock.Setup(comm => comm.Handle(It.IsAny<TInput>(), default))
                .Returns(result);

            return mock;
        }
    }
}
