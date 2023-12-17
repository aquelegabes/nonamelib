using AutoMapper;

namespace NoNameLib.Api.Tests.Mock.Handlers;

public class TestDomainDeleteCommandHandler :
    BaseDeleteCommandHandler<TestModel, TestDomain, string>
{
    public TestDomainDeleteCommandHandler(
        IRepository<TestDomain> repository,
        IUnitOfWork unitOfWork,
        IMapper mapper) :
        base(repository, unitOfWork, mapper)
    {
    }
}
