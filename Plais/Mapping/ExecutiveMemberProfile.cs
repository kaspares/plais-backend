using AutoMapper;
using Plais.DTOs.ExecutiveMember;
using PLAIS.API.Models;

namespace Plais.Mapping
{
    public class ExecutiveMemberProfile : Profile
    {
        public ExecutiveMemberProfile()
        {
            CreateMap<ExecutiveMember, ExecutiveMemberDto>()
                .ForMember(dest => dest.Memberships, opt =>
                opt.MapFrom(src => src.Memberships));

            CreateMap<SaveExecutiveMemberDto, ExecutiveMember>()
                .ForMember(dest => dest.Memberships, opt => opt.Ignore());
        }
    }
}
