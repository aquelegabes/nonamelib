using AutoMapper;

namespace NoNameLib.Api.Tests.Mock.Handlers;

public class TestDomainEditCommandHandler :
    BaseEditCommandHandler<TestModel, TestDomain, string>
{
    public TestDomainEditCommandHandler(
        IRepository<TestDomain> repository,
        IUnitOfWork unitOfWork,
        IMapper _mapper,
        IQuery<TestDomain> query) :
        base(repository, unitOfWork, _mapper, query)
    {
    }
}
