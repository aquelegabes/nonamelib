using AutoMapper;
using NoNameLib.Api.Commands;
using NoNameLib.Domain.Interfaces;
using NoNameLib.Domain.Tests.PlayTest;

namespace NoNameLib.Api.Tests.PlayTest;

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
