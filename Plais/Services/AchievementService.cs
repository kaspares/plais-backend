using AutoMapper;
using Plais.Common;
using Plais.Data.Interfaces;
using Plais.Data.Repositories;
using Plais.DTOs.Achievement;
using Plais.Exceptions;
using Plais.Models;
using Plais.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Plais.Services
{
    public class AchievementService(ILogger<AchievementService> logger,
        IMapper mapper,
        IAchievementRepository achievementRepository) : IAchievementService
    {
        public async Task<Achievement> CreateAsync(SaveAchievementDto dto)
        {
            logger.LogInformation("Creating a new achievement {@Achievement}", dto);
            var achievement = mapper.Map<Achievement>(dto);

            achievement.DateCreated = DateTime.Now;

            await achievementRepository.AddAsync(achievement);
            await achievementRepository.SaveChangesAsync();
            return achievement;

        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Deleting a achievement with id: {AchievementId}", id);

            var achievement = await achievementRepository.GetByIdAsync(id);

            if (achievement == null)
            {
                throw new NotFoundException(nameof(Achievement), id.ToString());

            }

            await achievementRepository.DeleteAsync(achievement);
            await achievementRepository.SaveChangesAsync();
        }

        public async Task<List<Achievement>> GetAllAsync()
        {
            logger.LogInformation("Getting all achievements");
            var achievement = await achievementRepository.GetAllAsync();
            return achievement;
        }

        public async Task<Achievement?> GetByIdAsync(int id)
        {
            logger.LogInformation("Getting a achievement with id: {Id}", id);

            var achievement = await achievementRepository.GetByIdAsync(id);

            if (achievement == null)
            {
                throw new NotFoundException(nameof(Achievement), id.ToString());

            }

            return achievement;
        }

        public async Task UpdateAsync(int id, SaveAchievementDto dto)
        {
            logger.LogInformation("Updating a achievement with id: {Id}", id);

            var achievement = await achievementRepository.GetByIdAsync(id);

            if (achievement == null)
            {
                throw new NotFoundException(nameof(Achievement), id.ToString());

            }

            mapper.Map(dto, achievement);
            await achievementRepository.UpdateAsync(achievement, dto.Images);
            await achievementRepository.SaveChangesAsync();
           
        }
    }
}
