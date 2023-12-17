using AutoMapper;

namespace NoNameLib.Api.Tests.Mock
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
