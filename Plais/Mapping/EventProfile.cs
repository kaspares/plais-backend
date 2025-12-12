using AutoMapper;
using Plais.DTOs.Event;
using Plais.DTOs.EventGroup;
using Plais.Models;

namespace Plais.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<SaveEventDto, Event>();
            CreateMap<Event, EventSummaryDto>();
        }
    }
}
