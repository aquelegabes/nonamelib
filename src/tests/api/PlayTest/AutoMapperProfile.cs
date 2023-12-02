using AutoMapper;
using NoNameLib.Domain.Tests.PlayTest;

namespace NoNameLib.Api.Tests.PlayTest
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TestDomain, TestModel>()
                .ReverseMap()
                .ForMember(_ => _.Id, _ => _.Ignore());
        }
    }
}
