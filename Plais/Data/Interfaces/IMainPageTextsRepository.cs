using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface IMainPageTextsRepository
    {
        Task<List<MainPageText>> GetAllAsync();
        Task<MainPageText?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
