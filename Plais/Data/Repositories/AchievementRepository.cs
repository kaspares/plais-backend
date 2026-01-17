using Microsoft.EntityFrameworkCore;
using Plais.Data.Interfaces;
using Plais.Models;
using PLAIS.API.Data;
using PLAIS.API.Models;

namespace Plais.Data.Repositories
{
    public class AchievementRepository(PlaisDbContext dbContext) : IAchievementRepository
    {
        public async Task AddAsync(Achievement achievement)
        {
            await dbContext.Achievements.AddAsync(achievement);
        }

        public async Task DeleteAsync(Achievement bulletin)
        {
            dbContext.Remove(bulletin);
        }

        public async Task<List<Achievement>> GetAllAsync()
        {
            var achievements = await dbContext.Achievements.Include(a => a.Images)
                .OrderBy(a => a.Position)
                .ToListAsync();
                

            return achievements;
        }

        public async Task<Achievement?> GetByIdAsync(int id)
        {
            var achievement = await dbContext.Achievements.Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id);
            return achievement;
        }

        public async Task<Achievement?> GetByPositionAsync(int position)
        {
            var achivevement = await dbContext.Achievements.FirstOrDefaultAsync(a => a.Position == position);
            return achivevement;
        }

        public async Task IncrementAllPositionsAsync()
        {
            await dbContext.Achievements.ExecuteUpdateAsync(s => s.SetProperty(a => a.Position, a => a.Position + 1));
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Achievement achievement, List<string> photoFileNames)
        {
            var existingPhotos = await dbContext.AchievementImages
           .Where(p => p.AchievementId == achievement.Id)
           .ToListAsync();

            dbContext.AchievementImages.RemoveRange(existingPhotos);

            achievement.Images = photoFileNames.Select(fileName => new AchievementImage
            {
                PhotoFileName = fileName,
                AchievementId = achievement.Id
            }).ToList();

            await dbContext.SaveChangesAsync();
        }
    }
}
