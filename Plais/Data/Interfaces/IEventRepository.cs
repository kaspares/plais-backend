using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetByIdAsync(int id);
        Task<List<Event>> GetLatestFour();
        Task AddAsync(Event eventEntity);
        Task DeleteAsync(Event eventEntity);
        Task<Event?> GetByPositionAsync(int position);
        Task IncrementAllEventPositionsInGroupAsync(int eventGroupId);
        Task SaveChangesAsync();
    }
}
