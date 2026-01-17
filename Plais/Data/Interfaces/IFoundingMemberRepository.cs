using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IFoundingMemberRepository
    {
        Task<List<FoundingMembers>> GetAllAsync();
        Task<FoundingMembers?> GetByIdAsync(int id);
        Task AddAsync(FoundingMembers member);
        Task DeleteAsync(FoundingMembers member);
        Task SaveChangesAsync();
    }
}
