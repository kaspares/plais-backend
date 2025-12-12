using AutoMapper;
using Plais.Data.Interfaces;
using Plais.Data.Repositories;
using Plais.DTOs.MainPageText;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;

namespace Plais.Services
{
    public class MainPageTextService(ILogger<MainPageTextService> logger,
        IMapper mapper,
        IMainPageTextsRepository mainPageTextsRepository) : IMainPageTextService
    {
        public async Task<List<MainPageTextDto>> GetAllAsync()
        {
            logger.LogInformation("Getting all texts");

            var texts = await mainPageTextsRepository.GetAllAsync();

            var textsDto = mapper.Map<List<MainPageTextDto>>(texts);
            return textsDto;
        }

        public async Task<MainPageTextDto?> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting text with id: {Id}", id);

            var text = await mainPageTextsRepository.GetByIdAsync(id);

            if (text == null)
            {
                throw new NotFoundException(nameof(MainPageText), id.ToString());

            }

            var textDto = mapper.Map<MainPageTextDto>(text);
            return textDto;
        }

        public async Task UpdateByIdAsync(int id, UpdateMainPageTextDto dto)
        {
            logger.LogInformation("Updating text with id: {Id}", id);

            var text = await mainPageTextsRepository.GetByIdAsync(id);
            if (text == null)
            {
                throw new NotFoundException(nameof(MainPageText), id.ToString());
            }

            mapper.Map(dto, text);
            await mainPageTextsRepository.SaveChangesAsync();
        }
    }
}
