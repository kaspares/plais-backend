using Plais.Common;
using Plais.DTOs.EventGroup;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IEventGroupService
    {
        Task<PagedResult<EventGroupDto>> GetAllAsync(ResourceQuery query);
        Task<EventGroupDto> GetEventGroupByIdAsync(int id);
        Task<EventGroupDto> CreateAsync(SaveEventGroupDto dto);
        Task UpdateAsync(int id, SaveEventGroupDto dto);
        Task DeleteAsync(int id);

    }
}
