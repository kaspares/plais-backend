using Plais.DTOs.FoundingMembers;
using Plais.DTOs.CurrentMember;

namespace Plais.Services.Interfaces
{
    public interface IFoundingMemberService
    {
        Task<FoundingMembersDto> CreateAsync(SaveFoundingMembersDto dto);
        Task<List<FoundingMembersDto>> GetAllAsync();

        Task<FoundingMembersDto> GetByIdAsync(int id);

        Task UpdateAsync(int id, SaveFoundingMembersDto dto);

        Task DeleteAsync(int id);
    }
}
