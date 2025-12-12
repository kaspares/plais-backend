using Plais.DTOs.ByLaws;
using Plais.DTOs.MainPageText;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IMainPageTextService
    {
        Task<List<MainPageTextDto>> GetAllAsync();
        Task<MainPageTextDto?> GetByIdAsync(int id);
        Task UpdateByIdAsync(int id, UpdateMainPageTextDto dto);
    }
}
