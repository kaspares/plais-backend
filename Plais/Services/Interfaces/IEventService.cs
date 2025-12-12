using Plais.DTOs.Event;
using Plais.DTOs.EventGroup;

namespace Plais.Services.Interfaces
{
    public interface IEventService
    {
        Task<EventDto> GetEventByIdAsync(int id);
        Task<List<EventSummaryDto>> GetLatestFour();
        Task<EventDto> CreateAsync(SaveEventDto dto);
        Task UpdateAsync(int id, SaveEventDto dto);
        Task DeleteAsync(int id);
    }
}
