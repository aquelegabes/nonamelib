using AutoMapper;

namespace NoNameLib.Api.Tests.Mock.Handlers;

public class TestDomainCreateCommandHandler :
    BaseCreateCommandHandler<TestModel, TestDomain>
{
    public TestDomainCreateCommandHandler(
        IRepository<TestDomain> repository,
        IUnitOfWork unitOfWork,
        IMapper _mapper) :
        base(repository, unitOfWork, _mapper)
    {
    }
}