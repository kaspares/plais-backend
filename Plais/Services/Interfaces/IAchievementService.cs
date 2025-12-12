using Plais.Common;
using Plais.DTOs.Achievement;
using Plais.DTOs.Bulletin;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IAchievementService
    {
        Task<List<Achievement>> GetAllAsync();
        Task<Achievement?> GetByIdAsync(int id);
        Task<Achievement> CreateAsync(SaveAchievementDto dto);
        Task UpdateAsync(int id, SaveAchievementDto dto);
        Task DeleteAsync(int id);
    }
}
