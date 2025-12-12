using Plais.Common;
using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IAchievementRepository
    {
        Task<List<Achievement>> GetAllAsync();
        Task<Achievement?> GetByIdAsync(int id);
        Task AddAsync(Achievement achievement);
        Task UpdateAsync(Achievement achievement, List<string> photoFileNames);
        Task DeleteAsync(Achievement bulletin);
        Task SaveChangesAsync();
    }
}
