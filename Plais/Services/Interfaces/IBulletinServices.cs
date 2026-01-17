using Plais.Common;
using Plais.DTOs.Bulletin;
using Plais.Models;

namespace Plais.Services.Interfaces
{
    public interface IBulletinServices
    {
        Task<PagedResult<Bulletin>> GetAllAsync(ResourceQuery query);
        Task<Bulletin?> GetByIdAsync(int id);
        Task<List<BulletinSummaryDto>> GetFourLatestAsync();
        Task<Bulletin> CreateAsync(SaveBulletinDto dto);
        Task UpdateAsync(int id, EditBulletinDto dto);
        Task DeleteAsync(int id);
    }
}
