using AutoMapper;
using Plais.DTOs.MainPageCarousel;
using Plais.Models;

namespace Plais.Mapping
{
    public class MainPageCarouselProfile : Profile
    {
        public MainPageCarouselProfile()
        {
            CreateMap<MainPageCarousel, MainPageCarouselDto>();
            CreateMap<SaveMainPageCarouselDto, MainPageCarousel>();
        }
    }
}
