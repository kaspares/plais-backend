using AutoMapper;
using Plais.DTOs.Cadence;
using Plais.DTOs.ExecutiveMember;
using PLAIS.API.Models;

namespace Plais.Mapping
{
    public class CadenceProfile : Profile
    {
        public CadenceProfile()
        {
            CreateMap<Cadence, CadenceDto>()
                .ForMember(dest => dest.MemberIds,
                opt => opt.MapFrom(src =>
                src.Members.Select(s => s.ExecutiveMemberId)));

            CreateMap<SaveCadenceDto, Cadence>();

            CreateMap<Cadence, CadanceWithMembersDto>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members));

            CreateMap<CadenceMembership, ExecutiveMemberInCadanceDto>()
                .ForMember(dest => dest.ExecutiveMemberId, opt => opt.MapFrom(src => src.ExecutiveMemberId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ExecutiveMember.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.ExecutiveMember.Phone))
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.ExecutiveMember.About))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.PhotoFileName, opt => opt.MapFrom(src => src.PhotoFileName));
        }

    }
    
}
