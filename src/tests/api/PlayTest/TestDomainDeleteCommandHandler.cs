using AutoMapper;
using NoNameLib.Api.Commands;
using NoNameLib.Domain.Interfaces;
using NoNameLib.Domain.Tests.PlayTest;

namespace NoNameLib.Api.Tests.PlayTest;

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
