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

        public static Mock<IDispatcher> GetDispatcher<TInput>(
            TInput input)
            where TInput : class
        {
            var mock = new Mock<IDispatcher>();

            mock.Setup(disp => disp.Dispatch(input));

            return mock;
        }

        public static Mock<IResultDispatcher> GetDispatcher<TInput, TOutput>(
            TInput input)
            where TInput : class
        {
            var mock = new Mock<IResultDispatcher>();
            object? instance;

            try
            {
                instance = Activator.CreateInstance(typeof(TOutput));
            }
            catch (MissingMethodException)
            {
                instance = default(TOutput);
                if (typeof(string) == typeof(TOutput))
                    instance = "ok";
            }

            mock.Setup(disp => disp.Dispatch(input, typeof(TOutput)))
                .Returns(new DispatchResult(instance, typeof(TOutput)));

            return mock;
        }

        public static Mock<IAsyncResultDispatcher> GetAsyncDispatcher<TInput, TOutput>(
            TInput input)
            where TInput : class
        {
            var mock = new Mock<IAsyncResultDispatcher>();
            object? instance;

            try
            {
                instance = Activator.CreateInstance(typeof(TOutput));
            }
            catch (MissingMethodException)
            {
                instance = default(TOutput);
                if (typeof(string) == typeof(TOutput))
                    instance = "ok";
            }

            mock.Setup(disp => disp.Dispatch(input, typeof(TOutput), default))
                .Returns(Task.FromResult(new DispatchResult(instance, typeof(TOutput))));
            return mock;
        }

        public static Mock<IAsyncDispatcher> GetAsyncDispatcher<TInput>(
            TInput input)
            where TInput : class
        {
            var mock = new Mock<IAsyncDispatcher>();

            mock.Setup(disp => disp.Dispatch(input, default));

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput>(
            ICommand<TInput> command)
            where TInput : class
        {
            var mock = new Mock<IServiceProvider>();

            mock.Setup(sp => sp.GetService(typeof(ICommand<TInput>)))
                .Returns(command);

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput, TOutput>(
            ICommand<TInput, TOutput> command)
            where TInput : class
            where TOutput : class
        {
            var mock = new Mock<IServiceProvider>();

            mock.Setup(sp => sp.GetService(typeof(ICommand<TInput, TOutput>)))
                .Returns(command);

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput>(
            IAsyncCommand<TInput> command)
            where TInput : class
        {
            var mock = new Mock<IServiceProvider>();

            mock.Setup(sp => sp.GetService(typeof(IAsyncCommand<TInput>)))
                .Returns(command);

            return mock;
        }

        public static Mock<IServiceProvider> GetServiceProviderMock<TInput, TOutput>(
            IAsyncCommand<TInput, TOutput> command)
            where TInput : class
            where TOutput : class
        {
            var mock = new Mock<IServiceProvider>();

            mock.Setup(sp => sp.GetService(typeof(IAsyncCommand<TInput, TOutput>)))
                .Returns(command);

            return mock;
        }
    }
}
