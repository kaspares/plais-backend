using AutoMapper;
using Plais.DTOs.Resource;
using Plais.Models;

namespace Plais.Mapping
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<ResourceItem, ResourceItemDto>();

            CreateMap<ResourceGroup, ResourceGroupDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<ResourceCategory, ResourceCategoryDetailsDto>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));

            CreateMap<ResourceCategory, ResourceCategoryDto>();

            CreateMap<UpdateResourceGroupWithItemsDto, ResourceGroup>();
            CreateMap<SaveResourceCategoryDto, ResourceCategory>();
            CreateMap<SaveResourceGroupDto, ResourceGroup>();
            CreateMap<SaveResourceItemDto, ResourceItem>();


        }
    }
}
