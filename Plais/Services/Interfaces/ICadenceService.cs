using Plais.DTOs.Cadence;

namespace Plais.Services.Interfaces
{
    public interface ICadenceService
    {
        Task<List<CadenceDto>> GetAllAsync();
        Task<CadenceDto> GetCadenceByIdAsync(int id);
        Task<CadenceDto> CreateAsync(SaveCadenceDto dto);
        Task UpdateAsync(int id, SaveCadenceDto dto);
        Task DeleteAsync(int id);

        Task<List<CadanceWithMembersDto>> GetAllCadencesWithMembersAsync();
    }
}
