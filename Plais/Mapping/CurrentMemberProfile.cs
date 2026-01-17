using AutoMapper;
using Plais.DTOs.CurrentMember;
using Plais.Models;

namespace Plais.Mapping
{
    public class CurrentMemberProfile : Profile
    {
        public CurrentMemberProfile()
        {
            CreateMap<SaveCurrentMemberDto, CurrentMembers>();
            CreateMap<CurrentMembers, CurrentMemberDto>();
        }
    }
}
