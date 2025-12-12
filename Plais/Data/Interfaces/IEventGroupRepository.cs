using Plais.Common;
using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IEventGroupRepository
    {
        Task<(List<EventGroup>, int)> GetAllAsync(ResourceQuery query);
        Task<EventGroup?> GetByIdAsync(int id);
        Task AddAsync(EventGroup eventGroup);
        Task DeleteAsync(EventGroup eventGroup);
        Task SaveChangesAsync();


    }
}
