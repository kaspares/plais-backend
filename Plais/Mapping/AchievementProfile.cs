using AutoMapper;
using Plais.DTOs.Achievement;
using Plais.DTOs.Bulletin;
using Plais.Models;

namespace Plais.Mapping
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            CreateMap<SaveAchievementDto, Achievement>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(fileName => new AchievementImage
                {
                    PhotoFileName = fileName
                })));
        }
    }
}
