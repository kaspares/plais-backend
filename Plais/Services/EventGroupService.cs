using AutoMapper;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.DTOs.EventGroup;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using PLAIS.API.Models;

namespace Plais.Services
{
    public class EventGroupService(ILogger<EventGroupService> logger,
        IMapper mapper,
        IEventGroupRepository eventGroupRepository) : IEventGroupService
    {
        public async Task<EventGroupDto> CreateAsync(SaveEventGroupDto dto)
        {

            logger.LogInformation("Creating a new event group {@EventGroup}", dto);

            var eventGroup = mapper.Map<EventGroup>(dto);
            eventGroup.DateCreated = DateTime.Now;

            await eventGroupRepository.AddAsync(eventGroup);
            await eventGroupRepository.SaveChangesAsync();

            var eventGroupDto = mapper.Map<EventGroupDto>(eventGroup);

            return eventGroupDto;

        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting event group with id: {Id}", id);

            var eventGroup = await eventGroupRepository.GetByIdAsync(id);

            if (eventGroup == null)
            {
                throw new NotFoundException(nameof(EventGroup), id.ToString());
            }
            await eventGroupRepository.DeleteAsync(eventGroup);
            await eventGroupRepository.SaveChangesAsync();
        }

        public async Task<PagedResult<EventGroupDto>> GetAllAsync(ResourceQuery query)
        {
            logger.LogInformation("Getting all event groups with events");

            var (eventGroups, totalCount) = await eventGroupRepository.GetAllAsync(query);

            var eventGroupsDto = mapper.Map<List<EventGroupDto>>(eventGroups);

            return new PagedResult<EventGroupDto>(eventGroupsDto, totalCount, query.PageSize, query.PageNumber);
        }

        public async Task<EventGroupDto> GetEventGroupByIdAsync(int id)
        {
            logger.LogInformation("Getting  event group with id: {Id}", id);

            var eventGroup = await eventGroupRepository.GetByIdAsync(id);

            if (eventGroup == null)
            {
                throw new NotFoundException(nameof(EventGroup), id.ToString());
            }

            var eventGroupDto = mapper.Map<EventGroupDto>(eventGroup);
            return eventGroupDto;

        }

        public async Task UpdateAsync(int id, SaveEventGroupDto dto)
        {
            logger.LogInformation("Updating event group with id: {Id} with {@EventGroup}", id, dto);

            var eventGroup = await eventGroupRepository.GetByIdAsync(id);

            if (eventGroup == null)
            {
                throw new NotFoundException(nameof(EventGroup), id.ToString());
            }

            mapper.Map(dto, eventGroup);

            await eventGroupRepository.SaveChangesAsync();

        }
    }
}
