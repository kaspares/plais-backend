using AutoMapper;
using Plais.DTOs.ByLaws;
using Plais.DTOs.FoundingMembers;
using Plais.Models;

namespace Plais.Mapping
{
    public class ByLawsProfile : Profile
    {
        public ByLawsProfile()
        {
            CreateMap<ByLawsDto, ByLaws>();
            CreateMap<ByLaws, ByLawsDto>();
        }
    }
}
