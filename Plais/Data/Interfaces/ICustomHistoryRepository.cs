using Plais.Models;

namespace Plais.Data.Interfaces
{
    public interface ICustomHistoryRepository
    {
        Task<History> GetAsync();
        Task SaveChangesAsync();
    }
}
