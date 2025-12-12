using AutoMapper;
using Plais.DTOs.EventGroup;
using Plais.Models;

namespace Plais.Mapping
{
    public class EventGroupProfile : Profile
    {
        public EventGroupProfile()
        {

            CreateMap<EventGroup, EventGroupDto>();
            CreateMap<SaveEventGroupDto, EventGroup>();

        }
    }
}
