using AutoMapper;
using Plais.DTOs.Bulletin;
using Plais.Models;

namespace Plais.Mapping
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<SaveBulletinDto, Bulletin>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PhotoFileNames.Select(fileName => new BulletinPhoto
                {
                    PhotoFileName = fileName
                })));

            CreateMap<EditBulletinDto, Bulletin>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PhotoFileNames.Select(fileName => new BulletinPhoto
                {
                    PhotoFileName = fileName
                })));

            CreateMap<Bulletin, BulletinSummaryDto>();
        }
    }
}
