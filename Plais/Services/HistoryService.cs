using AutoMapper;
using Plais.Data.Interfaces;
using Plais.Data.Repositories;
using Plais.DTOs.ByLaws;
using Plais.DTOs.History;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class HistoryService(ILogger<HistoryService> logger,
        IMapper mapper,
        ICustomHistoryRepository historyRepository) : IHistoryService
    {
        public async Task<HistoryDto> GetAsync()
        {
            logger.LogInformation("Getting History");

            var history = await historyRepository.GetAsync();

            var result = mapper.Map<HistoryDto>(history);

            return result;
        }

        public async Task UpdateAsync(HistoryDto dto)
        {
            logger.LogInformation("Updating History with {@History}", dto);

            var existing = await historyRepository.GetAsync();

            existing.Content = dto.Content;
            await historyRepository.SaveChangesAsync();
        }
    }
}
