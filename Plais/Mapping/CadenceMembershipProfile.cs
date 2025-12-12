using PLAIS.API.Models;
using AutoMapper;
using Plais.DTOs.CadenceMembership;

namespace Plais.Mapping
{
    public class CadenceMembershipProfile : Profile
    {
        public CadenceMembershipProfile()
        {
            CreateMap<CadenceMembership, CadenceMembershipDto>();
        }
    }
}
