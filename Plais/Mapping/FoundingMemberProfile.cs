using AutoMapper;
using Plais.DTOs.FoundingMembers;
using Plais.DTOs.CurrentMember;
using Plais.Models;

namespace Plais.Mapping
{
    public class FoundingMemberProfile : Profile
    {
        public FoundingMemberProfile()
        {
            CreateMap<SaveFoundingMembersDto, FoundingMembers>();
            CreateMap<FoundingMembers, FoundingMembersDto>();
        }
    }
}
