using Plais.DTOs.CurrentMember;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IMemberServices
    {
        Task<CurrentMemberDto> CreateAsync(SaveCurrentMemberDto dto);
        Task<List<CurrentMemberDto>> GetAllAsync();

        Task<CurrentMemberDto> GetByIdAsync(int id);

        Task UpdateAsync(int id, SaveCurrentMemberDto dto);

        Task DeleteAsync(int id);
    }
}
