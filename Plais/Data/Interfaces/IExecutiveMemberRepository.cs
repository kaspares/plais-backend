using Plais.DTOs;
using PLAIS.API.Models;

namespace Plais.Data.Interfaces
{
    public interface IExecutiveMemberRepository
    {
        Task<List<ExecutiveMember>> GetAllAsync();
        Task<ExecutiveMember?> GetByIdAsync(int id); 
        Task AddAsync(ExecutiveMember executiveMember);
        Task SaveChangesAsync();
        Task DeleteAsync(ExecutiveMember executiveMember);
        Task<CadenceMembership?> GetMembershipByCadenceAndPositionAsync(int cadenceId, int position);
    }
}
