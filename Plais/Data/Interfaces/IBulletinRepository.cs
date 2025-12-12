using Plais.Common;
using Plais.Models;
using PLAIS.API.Models;

namespace Plais.Data.Interfaces
{
    public interface IBulletinRepository
    {
        Task<(List<Bulletin>, int)> GetAllAsync(ResourceQuery query);
        Task<Bulletin?> GetByIdAsync(int id);
        Task<List<Bulletin>> GetFourLatestAsync();
        Task AddAsync(Bulletin bulletin);
        Task UpdateAsync(Bulletin bulletin, List<string> photoFileNames);
        Task DeleteAsync(Bulletin bulletin);
    }
}
