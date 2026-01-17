using AutoMapper;
using Plais.DTOs.ByLaws;
using Plais.DTOs.History;
using Plais.Models;

namespace Plais.Mapping
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<HistoryDto, History>();
            CreateMap<History, HistoryDto>();
        }
    }
}
