using Plais.Models;
using PLAIS.API.Models;

namespace Plais.Data.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<CurrentMembers>> GetAllAsync();
        Task<CurrentMembers?> GetByIdAsync(int id);
        Task AddAsync(CurrentMembers member);
        Task DeleteAsync(CurrentMembers member);
        Task SaveChangesAsync();
    }
}
