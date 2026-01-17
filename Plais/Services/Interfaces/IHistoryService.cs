using Plais.DTOs.ByLaws;
using Plais.DTOs.History;

namespace Plais.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetAsync();
        Task UpdateAsync(HistoryDto dto);
    }
}
