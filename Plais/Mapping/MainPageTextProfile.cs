using AutoMapper;
using Plais.DTOs.ByLaws;
using Plais.DTOs.MainPageText;
using Plais.Models;

namespace Plais.Mapping
{
    public class MainPageTextProfile : Profile
    {
        public MainPageTextProfile()
        {
            CreateMap<UpdateMainPageTextDto, MainPageText>();
            CreateMap<MainPageText, MainPageTextDto>();
        }
    }
}
