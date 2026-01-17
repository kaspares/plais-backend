using AutoMapper;
using Plais.Data.Interfaces;
using Plais.DTOs.ByLaws;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class ByLawsService(ILogger<ByLawsService> logger,
        IMapper mapper,
        IByLawsRepository byLawsRepository) : IByLawsService
    {
        public async Task<ByLawsDto> GetAsync()
        {
            logger.LogInformation("Geting ByLaws");

            var byLaw = await byLawsRepository.GetAsync();

            var result = mapper.Map<ByLawsDto>(byLaw);

            return result;
        }

        public async Task UpdateAsync(ByLawsDto dto)
        {
            logger.LogInformation("Updating ByLaws with {@ByLawsDto}", dto);

            var existing = await byLawsRepository.GetAsync();

            existing.Content = dto.Content;

            await byLawsRepository.SaveChangesAsync();

        }
    }
}
