using AutoMapper;

namespace NoNameLib.Api.Tests.Mock
{
    public static class MockObjects
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            return config.CreateMapper();
        }

        public static Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            var mock = new Mock<IUnitOfWork>();

            mock.Setup(uow => uow.BeginTransaction());
            mock.Setup(uow => uow.RollbackTransaction());
            mock.Setup(uow => uow.Commit());

            return mock;
        }
    }
}
