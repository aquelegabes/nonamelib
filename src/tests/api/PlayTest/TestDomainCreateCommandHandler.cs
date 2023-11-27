using AutoMapper;
using NoNameLib.Api.Commands;
using NoNameLib.Domain.Interfaces;
using NoNameLib.Domain.Tests.PlayTest;

namespace NoNameLib.Api.Tests.PlayTest;

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