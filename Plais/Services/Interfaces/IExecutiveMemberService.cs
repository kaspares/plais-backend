using Plais.DTOs.ExecutiveMember;

namespace Plais.Services.Interfaces
{
    public interface IExecutiveMemberService
    {
        Task<ExecutiveMemberDto> CreateAsync(SaveExecutiveMemberDto dto);
        Task<List<ExecutiveMemberDto>> GetAllAsync();

        Task<ExecutiveMemberDto> GetByIdAsync(int id);

        Task UpdateAsync(int id, SaveExecutiveMemberDto dto);

        Task DeleteAsync(int id);
    }
}
