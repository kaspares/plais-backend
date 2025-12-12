using AutoMapper;
using Plais.Data.Interfaces;
using Plais.Data.Repositories;
using Plais.DTOs.Event;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class EventService(ILogger<EventService> logger,
        IMapper mapper,
        IEventRepository eventRepository) : IEventService
    {
        public async Task<EventDto> CreateAsync(SaveEventDto dto)
        {
            var eventEntity = mapper.Map<Event>(dto);
            logger.LogInformation("Creating new event {@Event}", eventEntity);

            eventEntity.DateCreated = DateTime.Now;

            await eventRepository.AddAsync(eventEntity);
            await eventRepository.SaveChangesAsync();

            var eventEntityDto = mapper.Map<EventDto>(eventEntity);
            return eventEntityDto;

        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting event with id: {Id}", id);

            var eventEntity = await eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
            {
                throw new NotFoundException(nameof(Event), id.ToString());
            }

            await eventRepository.DeleteAsync(eventEntity);
            await eventRepository.SaveChangesAsync();

        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            logger.LogInformation("Getting event with id: {Id}", id);

            var eventEntity = await eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
            {
                throw new NotFoundException(nameof(Event), id.ToString());
            }

            var eventEntityDto = mapper.Map<EventDto>(eventEntity);
            return eventEntityDto;
        }

        public async Task<List<EventSummaryDto>> GetLatestFour()
        {
            logger.LogInformation("Getting latest events");

            var latestEvents = await eventRepository.GetLatestFour();

            var latestEventsDto = mapper.Map<List<EventSummaryDto>>(latestEvents);
            return latestEventsDto;
        }

        public async Task UpdateAsync(int id, SaveEventDto dto)
        {
            logger.LogInformation("Updating event with id: {Id} with {@Event}", id, dto);

            var eventEntity = await eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
            {
                throw new NotFoundException(nameof(EventGroup), id.ToString());
            }

            mapper.Map(dto, eventEntity);

            await eventRepository.SaveChangesAsync();

        }
    }
}
